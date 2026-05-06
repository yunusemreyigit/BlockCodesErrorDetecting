using System;

namespace BlockCodesErrorDetecting.Models;

public class Receiver
{
    private Checker _checker;
    public uint Dataword { get; private set; }

    public Receiver(IMethod method)
    {
        _checker = new Checker(method);
    }
    /// <summary>
    /// Accepts  the codeword that sender sent
    /// </summary>
    /// <param name="codeword"></param>
    /// <returns>Returns if extracting dataword is okey</returns>
    public bool Receive(uint codeword)
    {
        if (_checker.Check(codeword))
        {
            Dataword = _checker.ExtractDataword(codeword);
            return true;
        }
        else
        {
            Dataword = 0;
            return false;
        }
    }
}