using System;
using BlockCodesErrorDetecting.Models.CorruptionTypes;
using BlockCodesErrorDetecting.Models.Methods;

namespace BlockCodesErrorDetecting.Models;

public class Simulation
{
    public uint ReceivedDataword { get; private set; }
    public int DatawordWidth { get; private set; }
    public bool IsThereCorruption { get; private set; }
    private Sender _sender;
    private Medium _medium;
    private Receiver _receiver;
    private string _output;
    /// <summary>
    /// Encoding type on the sender
    /// </summary>
    public IMethod Method
    {
        set
        {
            _sender =  new Sender(value);
            _receiver = new Receiver(value);
        }
    }
    /// <summary>
    /// Corruption type in medium will be simulated.
    /// </summary>
    public ICorruption Corrupt
    {
        set
        {
            _medium = new Medium(value);
        }
    }
    /// <summary>
    /// Start the simulation
    /// </summary>
    /// <param name="sendDataword">Dataword to be sent.
    /// Can be 1-31 bit for partiy check method, 1-16 bit for block coding method.</param>
    public void Simulate(uint sendDataword)
    {
        DatawordWidth = CalculateSizeIn2(sendDataword);
        _output = "";
        
        _output += "Send dw: " + BitOperations.BitToString(sendDataword, DatawordWidth) + "\n";
        uint codeword =  _sender.Send(sendDataword);
        int sizeOfCodeword = CalculateSizeIn2(codeword);
        _output += "Generated cw: " + BitOperations.BitToString(codeword, sizeOfCodeword) + "\n";
        _medium.Corrupt(ref codeword, sizeOfCodeword);
        _output += "Corrupted cw: " + BitOperations.BitToString(codeword, sizeOfCodeword) + "\n";
        IsThereCorruption = !_receiver.Receive(codeword);
        ReceivedDataword = _receiver.Dataword;
        _output += "Received dw: " + BitOperations.BitToString(ReceivedDataword, DatawordWidth) + "\n";
        _output +=  IsThereCorruption == true ? "Error is detected!" : "No Error occured!";
        _output += "\n-------------------------\n";
        Console.WriteLine(_output);
    }
    /// <summary>
    /// Calculates have many  digits have the number in base 2
    /// </summary>
    /// <param name="number"></param>
    /// <returns>number of digits</returns>
    private int CalculateSizeIn2(uint number)
    {
        if (number == 0) return 1;
        return (int)Math.Floor(Math.Log(number, 2)) + 1;   
    }

    public string PrintOutput()
    {
        return _output;
    }
}