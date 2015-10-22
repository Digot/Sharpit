using System;
using System.Threading.Tasks;

namespace Sharpit.Command
{
    public class ConsoleReader
    {

        public static void Read(string prefix)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if(Console.CursorLeft== 0) Console.Write(prefix+ " ");
                    string inputLine = Console.ReadLine();
                    Console.WriteLine("Input: " + inputLine);
                }
            });
        }
    }
}