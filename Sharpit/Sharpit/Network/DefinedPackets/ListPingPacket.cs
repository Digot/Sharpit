using Sharpit.Network.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpit.Network.DefinedPackets
{
    class ListPingPacket : Packet.Packet 
    {
        public ListPingPacket() : base(PacketType.ListPing)
        {

        }


    }
}
