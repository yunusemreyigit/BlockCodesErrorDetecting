using System;

namespace BlockCodesErrorDetecting.Models.CorruptionTypes;

public class Burst: ICorruption
{
    public void Execute(ref uint codeword, int wordSize)
    {
        var random = new Random();
        for (int i = 0; i < wordSize; i++)
        {
            var index = random.Next(2);
            if(index == 1) 
                codeword = BitOperations.ToggleBit(codeword, i);
        }
    }
}