using Sharpit.Network.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Sharpit.Network.DefinedPackets;
using System.IO;

namespace Sharpit.Network
{
    class ListPingPacketHandler : PacketHandler
    {

        public ListPingPacketHandler() : base(typeof(ListPingPacket))
        {

        }

        public override void Receive(Socket socket, Packet.Packet packet)
        {
            Console.WriteLine("Handling ListPing!");
            String response = File.ReadAllText("motd.txt");
            packet.Send(socket, response);
        }
    }
}
