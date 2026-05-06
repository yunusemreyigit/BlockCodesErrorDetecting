using System;
using System.Linq;

namespace BlockCodesErrorDetecting.Models;

public class Encoder
{
    public uint Codeword { get; private set; }
    private IMethod _method;

    public Encoder(IMethod method)
    {
        _method = method;
    }

    public void Encode(uint dataword)
    {
        Codeword = _method.EncodeDataword(dataword);
    }
}