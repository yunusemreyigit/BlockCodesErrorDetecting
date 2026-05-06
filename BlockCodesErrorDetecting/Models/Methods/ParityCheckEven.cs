namespace BlockCodesErrorDetecting.Models.Methods;

public class ParityCheckEven: IMethod
{
    public uint EncodeDataword(uint data)
    {
        int n = BitOperations.PopCount(data);
        data = BitOperations.SlideLeft(data, 1);    
        if (n % 2 == 0)
            data = BitOperations.ClearBit(data, 0);
        else
            data = BitOperations.SetBit(data, 0);

        return data;
    }

    public bool CheckCodeword(uint codeword)
    {
        var n = BitOperations.PopCount(codeword);
        if (n % 2 == 0)
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