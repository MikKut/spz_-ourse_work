using System.Collections.Generic;

namespace Protector.Encryption.TotalEncryption
{
    internal interface IEncryptor
    {
        List<byte[]> EncryptFile(List<byte[]> program);
        byte[] EncryptFile(byte[] program);
    }
}