using System;
using Avalonia.Media;

namespace BlockCodesErrorDetecting.Models;

public class Medium
{
    private ICorruption _corruption;

    public Medium(ICorruption corruption)
    {
        _corruption = corruption;
    }

    public void Corrupt(ref uint codeword, int wordSize)
    {
        _corruption.Execute(ref codeword, wordSize);
    }
}