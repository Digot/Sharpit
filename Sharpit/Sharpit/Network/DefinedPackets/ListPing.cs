using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sharpit.Network.DefinedPackets
{
    public class ListPing : Packet
    {
        private PingPayload payload;

        public ListPing(PingPayload payload) : base(0x00)
        {
            this.payload = payload;
        }

        public override void Read(ref IOHandler handler, byte[] buffer)
        {

        }

        public override void Handle(ref IOHandler handler)
        {

        }

        public void Send(ref IOHandler handler)
        {
            Console.WriteLine("SEND");
            string json = JsonConvert.SerializeObject(payload);
            handler.WriteString(json);
            handler.Flush(0);
        }

    }



    public class PingPayload
    {
        /// <summary>
        /// Protocol that the server is using and the given name
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public VersionPayload Version { get; set; }

        [JsonProperty(PropertyName = "players")]
        public PlayersPayload Players { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Motd { get; set; }

        /// <summary>
        /// Server icon, important to note that it's encoded in base 64
        /// </summary>
        [JsonProperty(PropertyName = "favicon")]
        public string Icon { get; set; }

        public PingPayload(VersionPayload version, PlayersPayload players, string motd, string icon)
        {
            Version = version;
            Players = players;
            Motd = motd;
            Icon = icon;
        }
    }

    public class VersionPayload
    {
        [JsonProperty(PropertyName = "protocol")]
        public int Protocol { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public VersionPayload(int protocol, string name)
        {
            Protocol = protocol;
            Name = name;
        }
    }

    public class PlayersPayload
    {
        [JsonProperty(PropertyName = "max")]
        public int Max { get; set; }

        [JsonProperty(PropertyName = "online")]
        public int Online { get; set; }

        [JsonProperty(PropertyName = "sample")]
        public List<Player> Sample { get; set; }

        public PlayersPayload(int max, int online, List<Player> sample)
        {
            Max = max;
            Online = online;
            Sample = sample;
        }
    }

    public class Player
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public Player(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }


}