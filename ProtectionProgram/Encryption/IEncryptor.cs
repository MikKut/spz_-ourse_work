namespace Protector.Encryption
{
    internal interface IEncryptor
    {
        List<byte[]> EncryptFile(List<byte[]> program);
        byte[] EncryptFile(byte[] program);
    }
}