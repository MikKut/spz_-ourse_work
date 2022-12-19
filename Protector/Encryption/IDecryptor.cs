﻿using System.Collections.Generic;

namespace Protector.Encryption
{
    internal interface IDecryptor
    {
        byte[] DecryptFile(byte[] program);
        List<byte[]> DecryptFile(List<byte[]> program);
    }
}