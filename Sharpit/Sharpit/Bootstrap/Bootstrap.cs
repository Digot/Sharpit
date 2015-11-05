using System;
using Sharpit.Util.Logging;

namespace Sharpit.Bootstrap
{
    public class Bootstrap
    {
        public static void Main(string[] args)
        {
            ///
            /// Main entry point of Sharpit
            /// 
   
            SharpitServer sharpitServer = new SharpitServer();
            //Console.WriteLine("Press a key to close the server!");
            Console.ReadKey();
        }
    }
}