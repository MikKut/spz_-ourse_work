using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protector.Encryption;

namespace Protector.Tools
{
    internal partial class FileOperator : IFileOperator
    {
        public void ReplaceInitialFileWithBinary(ref string directoryPath, IEncryptor encryptor)
        {
            // simple to zip
            // delete simple
            // zip to binary
            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException($"There is no such directory: {directoryPath}");
            }

            string newPath = FileNameOperation.ChangeSimpleToZip(directoryPath);
            ZipFile.CreateFromDirectory(directoryPath, newPath);
            Directory.Delete(directoryPath, true);
            directoryPath = FileNameOperation.ChangeZipToBinary(newPath);
            var info = encryptor.EncryptFile(File.ReadAllBytes(newPath));
            WriteBinaryFile(directoryPath, info);
            File.Delete(newPath);
        }
        public void OpenEncryptedFile(ref string filePath, IDecryptor decryptor)
        {
            if (filePath == null || decryptor == null)
            {
                throw new ArgumentNullException($"Argument(s) passed in {nameof(OpenEncryptedFile)} is null");
            }

            string oldPath = filePath;
            filePath = FileNameOperation.ChangeNameToBinary(filePath);
            System.IO.File.Move(oldPath, filePath);
            var data = decryptor.DecryptFile(File.ReadAllBytes(filePath));
            File.Delete(filePath);
            WriteBinaryFile(filePath, data);
            UnzipBinaryFile(filePath);
            File.Delete(filePath);
            filePath = oldPath;
        }
        public void CloseDecryptedFile(ref string filePath, IEncryptor encryptor)
        {
            ReplaceInitialFileWithBinary(ref filePath, encryptor);
        }
        private void ZipBinaryFile(string directoryPath)
        {
            ZipFile.CreateFromDirectory(directoryPath, FileNameOperation.ChangeBinaryToZip(directoryPath));
        }
        private void UnzipBinaryFile(string filePath)
        {
            ZipFile.ExtractToDirectory(filePath, FileNameOperation.DeleteExtension(filePath, FileNameOperation.BinExtension));
        }
        private byte[] GetByteArrayOfTheFile(string fileName)
        {
            byte[] buff = null;
            using (FileStream fs = new FileStream(fileName,
                                           FileMode.Open,
                                           FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    long numBytes = new FileInfo(fileName).Length;
                    buff = br.ReadBytes((int)numBytes);
                    return buff;
                }
            }
        }
        private void TurnBinaryToSimple(ref string filePath)
        {
            string newFilePath = FileNameOperation.DeleteExtension(filePath, FileNameOperation.BinExtension);
            System.IO.File.Move(filePath, newFilePath);
            filePath = newFilePath;
        }
        private static void WriteBinaryFile(string filePath, byte[] data)
        {
            using (var bw = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                bw.Write(data);
            }
        }
    }
}
