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
    internal abstract class Cryptor
    {
        private Aes _encryptorAlgorythm;
        private static IKeyGeter _keyGeter;
        protected Aes EncryptorAlgorythm { get => _encryptorAlgorythm; set => _encryptorAlgorythm = value; }
        protected static IKeyGeter KeyGeter { get => _keyGeter; private set => _keyGeter = value; }
        public Cryptor(IKeyGeter currentKeyGeter)
        {
            if (currentKeyGeter == null)
            {
                throw new ArgumentNullException($"{nameof(IKeyGeter)} {nameof(currentKeyGeter)} passed in {nameof(Encryptor)} is null");
            }

            _encryptorAlgorythm = Aes.Create();
            _keyGeter = currentKeyGeter;
        }
    }
}
