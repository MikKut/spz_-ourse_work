using Protector.Encryption;
using Protector.Encryption.TotalEncryption;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Protector.Tools
{
    internal partial class FileOperator : IFileOperator
    {
        public string OpenEncryptedFile(ref string filePath, IDecryptor decryptor)
        {
            if (filePath == null || decryptor == null)
            {
                throw new ArgumentNullException($"Argument(s) passed in {nameof(OpenEncryptedFile)} is null");
            }

            string oldPath = filePath;
            filePath = FileNameOperator.ChangeNameToBinary(filePath);
            System.IO.File.Move(oldPath, filePath);
            byte[] data = decryptor.DecryptFile(File.ReadAllBytes(filePath));
            File.Delete(filePath);
            WriteBinaryFile(filePath, data);
            UnzipBinaryFile(filePath);
            File.Delete(filePath);
            filePath = oldPath;
            return FileNameOperator.GetPathToTheExe(filePath);
        }
        public void CloseDecryptedFile(ref string filePath, IEncryptor encryptor)
        {
            ReplaceInitialFileWithBinary(ref filePath, encryptor);
        }
        private void ReplaceInitialFileWithBinary(ref string directoryPath, IEncryptor encryptor)
        {
            // simple to zip
            // delete simple
            // zip to binary
            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException($"There is no such directory: {directoryPath}");
            }

            string newPath = FileNameOperator.ChangeSimpleToZip(directoryPath);
            ZipFile.CreateFromDirectory(directoryPath, newPath);
            Directory.Delete(directoryPath, true);
            directoryPath = FileNameOperator.ChangeZipToBinary(newPath);
            byte[] info = encryptor.EncryptFile(File.ReadAllBytes(newPath));
            WriteBinaryFile(directoryPath, info);
            File.Delete(newPath);
        }
        private void ZipBinaryFile(string directoryPath)
        {
            ZipFile.CreateFromDirectory(directoryPath, FileNameOperator.ChangeBinaryToZip(directoryPath));
        }
        private void UnzipBinaryFile(string filePath)
        {
            ZipFile.ExtractToDirectory(filePath, FileNameOperator.DeleteExtension(filePath, FileNameOperator.BinExtension));
        }
        private byte[] GetByteArrayOfTheFile(string fileName)
        {
            using FileStream fs = new(fileName,
                                           FileMode.Open,
                                           FileAccess.Read);
            using BinaryReader br = new(fs);
            long numBytes = new FileInfo(fileName).Length;
            byte[] buff = br.ReadBytes((int)numBytes);
            return buff;
        }
        private void TurnBinaryToSimple(ref string filePath)
        {
            string newFilePath = FileNameOperator.DeleteExtension(filePath, FileNameOperator.BinExtension);
            System.IO.File.Move(filePath, newFilePath);
            filePath = newFilePath;
        }
        private static void WriteBinaryFile(string filePath, byte[] data)
        {
            using BinaryWriter bw = new(File.Open(filePath, FileMode.OpenOrCreate));
            bw.Write(data);
        }
    }
}
