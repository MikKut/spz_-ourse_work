using Protector.BiosInfo;
using Protector.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Protector.Encryption
{
    internal class Decryptor : Cryptor, IDecryptor
    {
        public Decryptor(IKeyGeter currentKeyGeter)
            : base(currentKeyGeter)
        {
        }

        public List<byte[]> DecryptFile(List<byte[]> program)
        {
            var data = new List<byte[]>();
            foreach (var byteAr in program)
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
