using System;
using System.Collections.Generic;
using System.Dynamic;

namespace BlockCodesErrorDetecting.Models.Methods;

public class BlockCoding: IMethod
{
    private int _codewordSize;

    public uint EncodeDataword(uint data)
    {
        // control bits will be set in 2 and its multiplies so size of codeword will be n + 1 at the beginning
        // and the first bit will be deleted by sliding the bits right by 1
        int k = CalculateSizeIn2(data);
        int m = GetControlBitCount(k);
        _codewordSize = k + m + 1;
        uint codeword = 0;

        var j = 0;
        for (int i = 1; i <= _codewordSize; i++)
        {
            if ((i & (i - 1)) == 0) continue;   // control bits will be set in 2 and its multiplies
            if (BitOperations.GetBit(data, j) == 1)
                codeword = BitOperations.SetBit(codeword, i);
            j++;
        }
        
        // Calculating control bits
        for (int i = 0; i < m; i++)
        {
            int pos = (int)Math.Pow(2, i);
            if(CalculateParity(codeword, pos) == 1)
                codeword = BitOperations.SetBit(codeword, pos);
        }

        return BitOperations.SlideRight(codeword, 1);
    }
    public bool CheckCodeword(uint code)
    {
        return CalculateSyndrome(code) == 0;
    }
    public uint ExtractDatawordFrom(uint code)
    {
        code = BitOperations.SlideLeft(code, 1);
        uint data = 0;
        for (int i = 1, j = 0; i <= _codewordSize; i++)
        {
            if ((i & (i - 1)) != 0)     // data is on the outside of 2 and its multiplies
            {
                if(BitOperations.GetBit(code, i) == 1)
                    data = BitOperations.SetBit(data, j);
                j++;
            }
        }
        return data;
    }
    /// <summary>
    /// Calculates and returns syndrome in integer
    /// </summary>
    /// <param name="code">Codeword that will be checked</param>
    /// <returns>0 if there is no syndrome</returns>
    private int CalculateSyndrome(uint code)
    {
        int syndrome = 0;
        int n = _codewordSize - 1;
        for (int i = 1; i <= n; i++)
        {
            if (BitOperations.GetBit(code, i-1) == 1) syndrome ^= i;
        }
        return syndrome;        
    }
    private int CalculateParity(uint codeword, int pos)
    {
        int parity = 0;
        for (int i = pos; i < _codewordSize; i++)
        {
            if (((i) & pos) != 0)
                parity ^= BitOperations.GetBit(codeword, i);
        }
        return parity;
    }

    private int GetControlBitCount(int k)
    {
        int m = 0;
        while (Math.Pow(2, m) < (k + m + 1)) m++;
        return m;
    }
    private int CalculateSizeIn2(uint number)
    {
        if (number == 0) return 1;
        return (int)Math.Floor(Math.Log(number, 2)) + 1;   
    }
}
