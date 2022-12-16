using Protector.KeyReceiving;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Protector.Encryption.TotalEncryption
{
    internal class Encryptor : Cryptor, IEncryptor
    {
        public Encryptor(IKeyGetter currentKeyGeter)
            : base(currentKeyGeter)
        {
        }
        public List<byte[]> EncryptFile(List<byte[]> program)
        {
            List<byte[]> data = new();
            foreach (byte[] byteAr in program)
            {
                data.Add(EncryptBytes(byteAr));
            }

            return data;
        }
        public byte[] EncryptFile(byte[] program)
        {
            return EncryptBytes(program);
        }
        private byte[] EncryptBytes(byte[] byteToEncrypt)
        {
            EncryptorAlgorythm.Key = KeyGeter.GetKey();
            return EncryptorAlgorythm.EncryptEcb(byteToEncrypt, PaddingMode.Zeros);
        }
    }
}
