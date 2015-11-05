using System;
using System.Diagnostics;
using Sharpit.Command;
using Sharpit.Configuration;
using Sharpit.Logging;
using Sharpit.Network.Server;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
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
            new McServer(config.Port);
            
            ///
            /// Done
            /// 
            stopwatch.Stop();
            logger.info("Done! Startup took " + stopwatch.ElapsedMilliseconds + "ms");

            ConsoleReader.Read(">");
        }

        public string GetStringFromImage(Image image)
        {
            if (image != null)
            {
                ImageConverter ic = new ImageConverter();
                byte[] buffer = (byte[])ic.ConvertTo(image, typeof(byte[]));
                return Convert.ToBase64String(
                    buffer,
                    Base64FormattingOptions.InsertLineBreaks);
            }
            else
                return null;
        }

        public void InvokeHandler(Packet packet)
        {

        }

        public static void Stop()
        {
            
        }
    }
}