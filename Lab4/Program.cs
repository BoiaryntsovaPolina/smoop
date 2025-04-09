using System.Text.RegularExpressions;
namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] values = { "aE:dC:cA:56:76:54", "01:23:45:67:89:Az", "00:1B:44:11:3A:B7", "G1:23:45:67:89:AB", "01:23:45:67:89" };
            string pattern = @"^([0-9A-Fa-f]{2}:){5}[0-9A-Fa-f]{2}$";

            foreach (string value in values)
            {
                if (Regex.IsMatch(value, pattern))
                    Console.WriteLine("{0} is a valid MAC address.", value);
                else
                    Console.WriteLine("{0}: Invalid MAC address", value);
            }

            Console.ReadKey();
        }
    }
}
