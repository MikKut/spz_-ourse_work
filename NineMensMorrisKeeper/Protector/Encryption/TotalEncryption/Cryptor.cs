using Protector.KeyReceiving;
using System;
using System.Security.Cryptography;

namespace Protector.Encryption.TotalEncryption
{
    internal abstract class Cryptor
    {
        protected Aes EncryptorAlgorythm { get; set; }
        protected static IKeyGetter KeyGeter { get; private set; }
        public Cryptor(IKeyGetter currentKeyGeter)
        {
            EncryptorAlgorythm = Aes.Create();
            KeyGeter = currentKeyGeter??throw new ArgumentNullException($"{nameof(IKeyGetter)} {nameof(currentKeyGeter)} passed in {nameof(Encryptor)} is null");
        }
    }
}
