using Protector.BiosInfo;
using Protector.Encryption;
using Protector.Tools;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Protector.Lifetime
{
    public static class Starter
    {
        private static IFileOperator _fileOperator;
        private static IEncryptor _encryptor;
        private static IDecryptor _decryptor;
        private static IKeyGeter _keyGeter;
        static Starter()
        {
            _keyGeter = new ProxyKeyGetter();
            _fileOperator = new FileOperator();
            _encryptor = new Encryptor(_keyGeter);
            _decryptor = new Decryptor(_keyGeter);
        }

        public static void Work(string pathToTheFile, Action strartTheProgram)
        {
            Start(ref pathToTheFile);
            LaunchProgram(strartTheProgram);
            End(ref pathToTheFile);
        }
        private static void CreateInitialVersionFromCode(string pathToDirectoryToConvert)
        {
            ThrowArgumentNullExceptionIfStringIsNull(pathToDirectoryToConvert);
            _fileOperator.ReplaceInitialFileWithBinary(ref pathToDirectoryToConvert, _encryptor);
        }

        public static void Start(ref string pathToDirectoryToConvert)
        {
            ThrowArgumentNullExceptionIfStringIsNull(pathToDirectoryToConvert);
            _fileOperator.OpenEncryptedFile(ref pathToDirectoryToConvert, _decryptor);
        }
        public static void LaunchProgram(Action startTheProgram)
        {
            if (startTheProgram == null)
            {
                throw new ArgumentNullException("Delegate for program strt is empty");
            }

            startTheProgram();
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
