using System;
using System.Linq;

namespace BlockCodesErrorDetecting.Models;

public class Checker
{
    private IMethod _method;

    public Checker(IMethod method)
    {
        _method = method;
    }

    public bool Check(uint codeword)
    {
        return _method.CheckCodeword(codeword);
    }
    public uint ExtractDataword(uint codeword)
    {
        return _method.ExtractDatawordFrom(codeword);
    }
}