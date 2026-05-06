namespace BlockCodesErrorDetecting.Models.EncodingMethods;

public class ParityCheckOdd: IMethod
{
    public uint EncodeDataword(uint data)
    {
        int n = BitOperations.PopCount(data);
        data = BitOperations.SlideLeft(data, 1);    
        if (n % 2 == 1)
            data = BitOperations.ClearBit(data, 0);
        else
            data = BitOperations.SetBit(data, 0);

        return data;
    }

    public bool CheckCodeword(uint code)
    {
        var n = BitOperations.PopCount(code);
        if (n % 2 == 1)
            return true;
        else
            return false;
    }

    public uint ExtractDatawordFrom(uint codeword)
    {
        codeword = BitOperations.SlideRight(codeword, 1);
        return codeword;
    }
}