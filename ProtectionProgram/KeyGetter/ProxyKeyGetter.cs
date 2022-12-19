using System.Runtime.InteropServices;
using Protector.BiosInfo;

namespace Protector.BiosInfo
{
    internal class ProxyKeyGetter : IKeyGeter
    {
        private IKeyGeter _keyGeter;
        public ProxyKeyGetter()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _keyGeter = new WindowsKeyGeter();
            }

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                throw new NotImplementedException($"There is no functionality for Linux, sorry :((");
            }

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                throw new NotImplementedException($"There is no way we will do something for Mac!!!! 凸( •̀_•́ )凸");
            }

            else
            {
                throw new NotImplementedException($"We don't even know what kind of OS you're using, bruh [¬º-°]¬");
            }
        }
        public byte[] GetKey()
        {
            return _keyGeter.GetKey();
        }
    }
}
