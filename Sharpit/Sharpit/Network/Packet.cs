using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Sharpit.Network.DefinedPackets;
using Sharpit.Util;

namespace Sharpit.Network
{
    public abstract class Packet
    {
        private static List<Packet> packets;

        static Packet()
        {
            packets = new List<Packet>()
            {
                new Handshake(),
                new Request()
            };
        }

        public static Packet getByType(int id, int size)
        {
            foreach (var packet in packets)
            {
                if ((int)packet.Type == id)
                {
                   // Console.WriteLine("PACKET: {0} - expect empty: {1} - size: {2}", packet.GetType().Name, packet.emptyDataExpected, size);
                    if (size >= 0 && size <= 1)
                    {
                        if (packet.emptyDataExpected)
                        {
                            return (Packet) packet.GetType().GetConstructor(new Type[] {}).Invoke(new object[] {});
                        }
                    }
                    else if(size > 1)
                    {
                        return (Packet)packet.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
                    }

                   

                }
            }

            return null;
        }

        public byte Type { get; set; }
        public bool emptyDataExpected { get; set; }


        public Packet(byte type)
        {
            this.Type = type;
            emptyDataExpected = false;
        }

        public Packet(byte type, bool _emptyDataExpected)
        {
            this.Type = type;
            emptyDataExpected = _emptyDataExpected;
        }

        public abstract void Read(ref IOHandler handler, Byte[] buffer);
        public abstract void Handle(ref IOHandler handler);

        public void Send(ref IOHandler handler, Action write)
        {

            handler.Flush((int)Type);
        }

    }
}