using System.Security.Cryptography;
using Protector.BiosInfo;
using Protector.Tools;

namespace Protector.Encryption
{
    internal class Encryptor : Cryptor, IEncryptor
    {
        public Encryptor(IKeyGeter currentKeyGeter)
            : base(currentKeyGeter)
        {
        }
        public List<byte[]> EncryptFile(List<byte[]> program)
        {
            List<byte[]> data = new List<byte[]>();
            foreach (var byteAr in program)
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
