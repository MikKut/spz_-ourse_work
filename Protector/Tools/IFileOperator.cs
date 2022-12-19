using Protector.Encryption;

namespace Protector.Tools
{
    internal interface IFileOperator
    {
        void CloseDecryptedFile(ref string filePath, IEncryptor encryptor);
        void OpenEncryptedFile(ref string filePath, IDecryptor decryptor);
        void ReplaceInitialFileWithBinary(ref string directoryPath, IEncryptor encryptor);
    }
}