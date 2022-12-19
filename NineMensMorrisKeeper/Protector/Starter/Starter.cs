using Protector.Encryption.TotalEncryption;
using Protector.KeyReceiving;
using Protector.Tools;
using System;

namespace Protector.Starter
{
    public static class Starter
    {
        private static readonly IFileOperator _fileOperator;
        private static readonly IEncryptor _encryptor;
        private static readonly IDecryptor _decryptor;
        private static readonly IKeyGetter _keyGeter;
        static Starter()
        {
            _keyGeter = new ProxyKeyGetter();
            _fileOperator = new FileOperator();
            _encryptor = new Encryptor(_keyGeter);
            _decryptor = new Decryptor(_keyGeter);
        }
        public static void Start(ref string pathToFile)
        {
            ThrowArgumentNullExceptionIfStringIsNull(pathToFile);
            _fileOperator.OpenEncryptedFile(ref pathToFile, _decryptor);
        }
        public static void End(ref string pathToFile)
        {
            ThrowArgumentNullExceptionIfStringIsNull(pathToFile);
            _fileOperator.CloseDecryptedFile(ref pathToFile, _encryptor);
        }
        private static void ThrowArgumentNullExceptionIfStringIsNull(string path)
        {
            if (path == null || path == string.Empty)
            {
                throw new ArgumentNullException("path is empty or null");
            }
        }
    }
}
