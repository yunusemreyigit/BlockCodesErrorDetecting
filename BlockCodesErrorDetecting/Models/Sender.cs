namespace BlockCodesErrorDetecting.Models;

public class Sender
{
    private Encoder _encoder;
    public Sender(IMethod method)
    {
        _encoder = new Encoder(method);
    }
    public uint Send(uint dataword)
    {
        // encoding dataword
        _encoder.Encode(dataword);
        return _encoder.Codeword;
    }
}