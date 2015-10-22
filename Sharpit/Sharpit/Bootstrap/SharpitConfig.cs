using Sharpit.Configuration;

namespace Sharpit.Bootstrap
{
    [FilePath("server.cfg")]
    public class SharpitConfig : Configuration.Configuration
    {
        [Comment("The port, the server will be started on")]
        public int Port { get; set; } = 25565;
    }
}