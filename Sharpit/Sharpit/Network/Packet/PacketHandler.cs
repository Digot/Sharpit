using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sharpit.Network.Packet
{
    abstract class PacketHandler 
    {

        private Type type;
        

        public PacketHandler(Type type)
        {
            this.type = type;
        }
         
        
        public abstract void Receive(Socket socket, Packet packet);
    }
}
