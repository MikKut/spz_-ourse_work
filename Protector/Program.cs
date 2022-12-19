using Protector.BiosInfo;
using Protector.Lifetime;
using Protector.Tools;
using System.Diagnostics;

namespace Protector
{
    internal class Program
    {
        public static void Main()
        {
            string st = GetGamePosition();
            var x = new FileOperator();
            Starter.Start(ref st);
            Process.Start("\\bin\\Debug\\net6.0-windows\\NineMensMorris.exe");
            Console.WriteLine("Something");
            Starter.End(ref st);
        }
        private static string GetGamePosition()
        {
            return Directory.GetCurrentDirectory().Replace("Protector\\bin\\Debug\\net6.0", "NineMensMorris");
        }
    }
}
