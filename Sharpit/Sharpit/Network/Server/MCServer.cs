using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Sharpit.Logging;

namespace Sharpit.Network.Server
{
    public class McServer
    {
        private Logger logger = Logger.get(typeof(McServer));
        private List<IOHandler> handlerList;

        public McServer(int port)
        {
            handlerList = new List<IOHandler>();
            startServer(port);
        }

        private void startServer(int port)
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            TcpListener serverSocket = new TcpListener(ipAddress,port);
            serverSocket.Start();
            logger.info("Socket server started!");
           

            while (true)
            {
                IOHandler handler = null;
                TcpClient clientSocket = serverSocket.AcceptTcpClient();


                try
                {
                   
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] buffer = new byte[4096];

                    

                    foreach (var ioHandler in handlerList)
                    {
                        if (clientSocket.Client.LocalEndPoint.Equals(ioHandler.clientSocket.Client.LocalEndPoint))
                        {
                            //Handler is already in the list
                            handler = ioHandler;
                        }
                    }
                 
                    networkStream.Read(buffer, 0,buffer.Length);

                    if (handler == null)
                    {
                        handler =  new IOHandler(networkStream, clientSocket);
                        handlerList.Add(handler);
                    }

                    var length = handler.ReadVarInt(buffer);
                    var packetID = handler.ReadVarInt(buffer);

                    Packet packet = Packet.getByType(packetID, length);
                    packet.Read(ref handler,buffer);
                    packet.Handle(ref handler);
                    Console.WriteLine("Received packet! " + packet);

                    
                   
                }
                catch (Exception)
                {
                    Console.WriteLine("Error! Disconnecting client! ");
                    clientSocket.Close();
                    if (handlerList.Contains(handler)) handlerList.Remove(handler);
                }
            }
        }
    }

}