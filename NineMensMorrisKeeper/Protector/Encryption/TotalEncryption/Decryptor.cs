using Protector.KeyReceiving;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Protector.Encryption.TotalEncryption
{
    internal class Decryptor : Cryptor, IDecryptor
    {
        public Decryptor(IKeyGetter currentKeyGeter)
            : base(currentKeyGeter)
        {
        }

        public List<byte[]> DecryptFile(List<byte[]> program)
        {
            List<byte[]> data = new();
            foreach (byte[] byteAr in program)
            {
                data.Add(DecryptBytes(byteAr));
            }

            return data;
        }
        public byte[] DecryptFile(byte[] program)
        {
            return DecryptBytes(program);
        }
        private byte[] DecryptBytes(byte[] byteToEncrypt)
        {
            EncryptorAlgorythm.Key = KeyGeter.GetKey();
            return EncryptorAlgorythm.DecryptEcb(byteToEncrypt, PaddingMode.Zeros);
        }
    }
}
