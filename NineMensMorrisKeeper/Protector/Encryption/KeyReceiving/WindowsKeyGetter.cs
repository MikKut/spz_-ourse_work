using System.Management;
using System.Text;

namespace Protector.KeyReceiving
{
    internal class WindowsKeyGeter : IKeyGetter
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
            ManagementObjectSearcher searcher = new(scope, query);
            string x1, x2;
            foreach (ManagementBaseObject _object in searcher.Get())
            {
                if (_object != null)
                {
                    x1 = _object.Properties[keySource1].Value.ToString();
                    x2 = _object.Properties[keySource2].Value.ToString();
                    byte[] x = Encoding.UTF8.GetBytes(x1 + x2);
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