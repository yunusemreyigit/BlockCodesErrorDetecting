namespace BlockCodesErrorDetecting.Models;

public interface IMethod
{
    uint EncodeDataword(uint data);
    bool CheckCodeword(uint code);
    uint ExtractDatawordFrom(uint code);
}