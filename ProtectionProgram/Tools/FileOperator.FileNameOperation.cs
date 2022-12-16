using Protector.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protector.Tools
{
    internal partial class FileOperator : IFileOperator
    {
        private static class FileNameOperation
        {
            public const string BinExtension = ".bin", ZipExtension = ".zip";
            public static string ChangeNameToBinary(string nameNotBinaryName)
            {
                if (nameNotBinaryName.EndsWith(BinExtension))
                {
                    return nameNotBinaryName;
                }

                return nameNotBinaryName + BinExtension;
            }
            public static string ChangeSimpleToZip(string pathName)
            {
                if (pathName.EndsWith(ZipExtension))
                {
                    return pathName;
                }

                return pathName + ZipExtension;
            }
            public static string ChangeBinaryToZip(string pathName)
            {
                if (pathName.EndsWith(ZipExtension))
                {
                    return pathName;
                }

                if (!pathName.EndsWith(BinExtension))
                {
                    throw new ArgumentException($"Cannot convert to zip: {pathName} does not end with {BinExtension}");
                }

                return pathName + ZipExtension;
            }
            public static string ChangeZipToBinary(string pathName)
            {
                if (pathName.EndsWith(BinExtension))
                {
                    return pathName;
                }

                if (!pathName.EndsWith(ZipExtension))
                {
                    throw new ArgumentException($"Cannot convert to bin: {pathName} does not end with {ZipExtension}");
                }

                return pathName.Remove(pathName.Length - BinExtension.Length);
            }
            public static string DeleteExtension(string nameWithExtension, string extensionToDelete)
            {
                if (nameWithExtension.EndsWith(extensionToDelete))
                {
                    return nameWithExtension.Remove(nameWithExtension.Length - extensionToDelete.Length);
                }

                throw new ArgumentException($"Cannot convert to simple: {nameWithExtension} does not end with {extensionToDelete}");
            }
            public static string ChangeSimpleToBinary(string path)
            {
                if (path.EndsWith(BinExtension))
                {
                    return path;
                }

                if (path.EndsWith(BinExtension))
                {
                    throw new ArgumentException($"Cannot convert to bin: {path} is already ended with {BinExtension}");
                }

                return path + BinExtension;
            }
        }
    }
}
