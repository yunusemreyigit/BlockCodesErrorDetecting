using System;

namespace BlockCodesErrorDetecting.Models.CorruptionTypes;

public class SingleBit: ICorruption
{
    public void Execute(ref uint codeword, int wordSize)
    {
        var random = new Random();
        int index = random.Next(wordSize);
        codeword = BitOperations.ToggleBit(codeword, index);
    }
}