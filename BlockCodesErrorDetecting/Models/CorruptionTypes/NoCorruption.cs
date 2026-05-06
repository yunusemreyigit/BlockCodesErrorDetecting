namespace BlockCodesErrorDetecting.Models.CorruptionTypes;

public class NoCorruption: ICorruption
{
    public void Execute(ref uint codeword, int wordSize){}
}