using System;

namespace Sharpit.Network.DefinedPackets
{
    public class Request : Packet
    {
        public Request() : base(0x00,true)
        {
        }

        public override void Read(ref IOHandler handler, byte[] buffer)
        {
            //Nothing to read
        }

        public override void Handle(ref IOHandler handler)
        {
            //Check state
            Console.WriteLine("REQUEST STATE:"  + handler.State);
            if (handler.State == 1)
            {
                //Send list ping
                Console.WriteLine("Sending list ping!");
                new ListPing(new PingPayload(
                    new VersionPayload(47, "Spigot"),
                    new PlayersPayload(20, 2, null),
                    "Test", null
                    )).Send(ref handler);
           }
        }

        public override string ToString()
        {
            return "Request()";
        }
    }
}