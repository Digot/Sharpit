using System;
using System.Diagnostics;
using Sharpit.Command;
using Sharpit.Configuration;
using Sharpit.Logging;
using Sharpit.Network.Server;
using System.Collections.Generic;
using Sharpit.Network;

namespace Sharpit.Bootstrap
{
    public class SharpitServer
    {

        private Logger logger = Logger.get(typeof(SharpitServer));
        private SharpitConfig config;
        private Dictionary<Type, PacketHandler> packetHandlers = new Dictionary<Type,PacketHandler>();

        public SharpitServer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            logger.info("Starting SharpitServer");

            ///
            /// Init configuration
            /// 
            logger.info("Initializing configuration!");
            config = ConfigManager.Load<SharpitConfig>();

            ///
            /// Start socket
            /// 
            logger.info("Starting server on port " + config.Port);
            //AsynchronousSocketListener.StartListening(config.Port);
            new McServer(config.Port);

            ///
            /// Done
            /// 
            stopwatch.Stop();
            logger.info("Done! Startup took " + stopwatch.ElapsedMilliseconds + "ms");

            ConsoleReader.Read(">");
        }

        public void InvokeHandler(Packet packet)
        {

        }

        public static void Stop()
        {
            
        }
    }
}