using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpit.Network.Packet
{
    abstract class PacketHandler<T>
    {
        public abstract void Receive(T packet);
    }
}
