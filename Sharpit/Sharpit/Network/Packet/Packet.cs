using Sharpit.Util;
using System.Net.Sockets;

namespace Sharpit.Network.Packet
{
    public class Packet
    {
        public byte Type { get; set; }


        public Packet(byte type)
        {
            this.Type = type;
        }

        public void Send(Socket handler, byte[] data)
        {
            ///
            /// Unencrypted Packets are structured like this
            /// 
            /// {Length (varint), PacketID (varint), Data (byte array)}
            ///
            /// The Length is defined by by data.length + packetid.length
            ///

            byte[] packetIDVarInt = VarIntConverter.GetVarintBytes(Type);
            byte[] dataLengthVarInt = VarIntConverter.GetVarintBytes(data.Length);

            int packetIDInt = VarIntConverter.ToInt32(packetIDVarInt);
            int dataLengthInt = VarIntConverter.ToInt32(dataLengthVarInt);

            int lengthInt = packetIDInt + dataLengthInt;

            byte packetID = VarIntConverter.ToByte(packetIDVarInt);
            byte length = VarIntConverter.ToByte(VarIntConverter.GetVarintBytes(lengthInt));

            int bufferOffset = 2;

            int packetBufferLength = bufferOffset + data.Length;
            byte[] finalPacketBuffer = new byte[packetBufferLength];

            finalPacketBuffer[0] = length;
            finalPacketBuffer[1] = packetID;

            for (int i = 0; i < data.Length; i++)
            {
                finalPacketBuffer[i + bufferOffset] = data[i];
            }

            handler.Send(finalPacketBuffer);
            System.Console.WriteLine("Sent!");
        }

        public void Send(Socket handler, string data)
        {
            Send(handler, StringByteConverter.GetBytes(data));
        }

    }
}