using Protector.Encryption;
using Protector.Encryption.TotalEncryption;

namespace Protector.Tools
{
    internal interface IFileOperator
    {
        void CloseDecryptedFile(ref string filePath, IEncryptor encryptor);
        string OpenEncryptedFile(ref string filePath, IDecryptor decryptor);
    }
}