using System;

namespace Protector.Tools
{
    internal partial class FileOperator : IFileOperator
    {
        private static class FileNameOperator
        {
            public const string BinExtension = ".bin", ZipExtension = ".zip";
            public static string ChangeNameToBinary(string nameNotBinaryName)
            {
                return nameNotBinaryName.EndsWith(BinExtension) ? nameNotBinaryName : nameNotBinaryName + BinExtension;
            }
            public static string ChangeSimpleToZip(string pathName)
            {
                return pathName.EndsWith(ZipExtension) ? pathName : pathName + ZipExtension;
            }
            public static string ChangeBinaryToZip(string pathName)
            {
                return pathName.EndsWith(ZipExtension)
                    ? pathName
                    : !pathName.EndsWith(BinExtension)
                    ? throw new ArgumentException($"Cannot convert to zip: {pathName} does not end with {BinExtension}")
                    : pathName + ZipExtension;
            }
            public static string ChangeZipToBinary(string pathName)
            {
                return pathName.EndsWith(BinExtension)
                    ? pathName
                    : !pathName.EndsWith(ZipExtension)
                    ? throw new ArgumentException($"Cannot convert to bin: {pathName} does not end with {ZipExtension}")
                    : pathName.Remove(pathName.Length - BinExtension.Length);
            }
            public static string DeleteExtension(string nameWithExtension, string extensionToDelete)
            {
                return nameWithExtension.EndsWith(extensionToDelete)
                    ? nameWithExtension.Remove(nameWithExtension.Length - extensionToDelete.Length)
                    : throw new ArgumentException($"Cannot convert to simple: {nameWithExtension} does not end with {extensionToDelete}");
            }
            public static string ChangeSimpleToBinary(string path)
            {
                return path.EndsWith(BinExtension)
                    ? path
                    : path.EndsWith(BinExtension)
                    ? throw new ArgumentException($"Cannot convert to bin: {path} is already ended with {BinExtension}")
                    : path + BinExtension;
            }
        }
    }
}
