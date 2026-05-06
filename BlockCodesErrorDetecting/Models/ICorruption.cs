namespace BlockCodesErrorDetecting.Models;

public interface ICorruption
{
    void Execute(ref uint codeword, int wordSize);
}