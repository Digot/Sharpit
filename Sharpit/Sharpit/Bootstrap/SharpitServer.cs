using System;
using System.Diagnostics;
using Sharpit.Configuration;
using Sharpit.Network.Server;
using Sharpit.Util.Logging;

namespace Sharpit.Bootstrap
{
    public class SharpitServer
    {

        private Logger logger = Logger.get(typeof(SharpitServer));
        private SharpitConfig config;

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
            AsynchronousSocketListener.StartListening(config.Port);

            ///
            /// Done
            /// 
            stopwatch.Stop();
            logger.info("Done! Startup took " + stopwatch.ElapsedMilliseconds + "ms");
        }
    }
}