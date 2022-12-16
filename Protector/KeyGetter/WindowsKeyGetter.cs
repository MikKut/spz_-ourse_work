using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Protector.BiosInfo
{
    internal class WindowsKeyGeter : IKeyGeter
    {
        private const int SizeOfTheKey = 32;
        private const string 
            scope = @"\\.\root\cimv2", 
            query = "SELECT * FROM Win32_BIOS",
            keySource1 = "ReleaseDate", 
            keySource2 = "SerialNumber";
        public byte[] GetKey()
        {
            byte[] result = new byte[SizeOfTheKey];
            var searcher = new ManagementObjectSearcher(scope, query);
            string x1, x2;
            foreach (var _object in searcher.Get())
            {
                if (_object != null)
                {
                    x1 = _object.Properties[keySource1].Value.ToString();
                    x2 = _object.Properties[keySource2].Value.ToString();
                    var x = Encoding.UTF8.GetBytes(x1 + x2);
                    for (int i = 0; i < SizeOfTheKey; i++)
                    {
                        result[i] = x[i];
                    }
                    return result;
                }
            }
            throw new ManagementException();
        }
    }
}