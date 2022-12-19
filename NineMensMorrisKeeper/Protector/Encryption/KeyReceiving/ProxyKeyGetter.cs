using System.Runtime.InteropServices;

namespace Protector.KeyReceiving
{
    internal class ProxyKeyGetter : IKeyGetter
    {
        private readonly IKeyGetter _keyGeter;
        public ProxyKeyGetter()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _keyGeter = new WindowsKeyGeter();
            }

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                throw new System.NotImplementedException($"There is no functionality for Linux, sorry :((");
            }

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                throw new System.NotImplementedException($"There is no way we will do something for Mac!!!! 凸( •̀_•́ )凸");
            }

            else
            {
                throw new System.NotImplementedException($"We don't even know what kind of OS you're using, bruh [¬º-°]¬");
            }
        }
        public byte[] GetKey()
        {
            return _keyGeter.GetKey();
        }
    }
}
