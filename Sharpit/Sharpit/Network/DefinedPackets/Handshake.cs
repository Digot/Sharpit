using System;
using static System.String;

namespace Sharpit.Network.DefinedPackets
{
    public class Handshake : Packet
    {

        private int protocolVersion;
        private string address;
        short clientPort;
        int nextState;

        public Handshake() : base(0x00)
        {

        }

        public override void Read(ref IOHandler handler, byte[] buffer)
        {
            protocolVersion = handler.ReadVarInt(buffer);
            int addressLength = handler.ReadVarInt(buffer);
            address = handler.ReadString(buffer, addressLength);
            clientPort = handler.ReadShort(buffer);
            nextState = handler.ReadVarInt(buffer);
        }

        public override void Handle(ref IOHandler handler)
        {
            //Set next state
            Console.WriteLine("Setting state");
            handler.State = nextState;
        }

        public override string ToString()
        {
            return Format("Handshake(ProtocolVersion={0}, Address={1}, Port={2}, NextState={3})", protocolVersion,
                address, clientPort, nextState);
        }
    }
}