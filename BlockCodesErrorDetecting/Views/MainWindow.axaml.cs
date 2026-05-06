using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BlockCodesErrorDetecting.Models;
using BlockCodesErrorDetecting.Models.CorruptionTypes;
using BlockCodesErrorDetecting.Models.EncodingMethods;
using BlockCodesErrorDetecting.Models.Methods;

namespace BlockCodesErrorDetecting.Views;

public partial class MainWindow : Window
{
    private Simulation _simulation;
    public MainWindow()
    {
        InitializeComponent();
        _simulation = new Simulation();
    }
    private void Send(object? sender, RoutedEventArgs e)
    {
        // Binary Check for the input
        var text = SenderDataword.Text;
        if(!IsBinary(text)) return;
        var number = Convert.ToUInt32(text,2);
        
        // Setting the parameters of the simulation
        if (ParityE.IsChecked == true) _simulation.Method = new ParityCheckEven();
        else if (ParityO.IsChecked == true) _simulation.Method = new ParityCheckOdd();
        else if (Block.IsChecked == true) _simulation.Method = new BlockCoding();

        if (NoError.IsChecked == true) _simulation.Corrupt = new NoCorruption();
        else if (Single.IsChecked == true) _simulation.Corrupt = new SingleBit();
        else if (Burst.IsChecked == true) _simulation.Corrupt = new Burst();
        
        // Simulation start
        _simulation.Simulate(number);
        
        // If there is a corruption print it on the output
        if(_simulation.IsThereCorruption)
            ReceivedDataword.Text = "Error is detected!";
        else
            ReceivedDataword.Text = BitOperations.BitToString(_simulation.ReceivedDataword, _simulation.DatawordWidth);
        
        // Output info printing
        Output.Text = _simulation.PrintOutput();
    }
    private bool IsBinary(string text) => text.All(c => c is '0' or '1');
}