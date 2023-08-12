using System.Net;

namespace BBR.Community.API
{
    public class Configuration
    {
        public BattleBitConfig BattleBit { get; set; } = new BattleBitConfig();
        public class BattleBitConfig
        {
            public string IP { get; set; } = "0.0.0.0";
            public int Port { get; set; } = 29294;
        }

        public EFCoreConfig EfCore { get; set; } = new EFCoreConfig();
        public class EFCoreConfig
        {
            public bool MigrateOnStartup { get; set; } = false;
        }

        public ContextConfig Context { get; set; } = new ContextConfig();
        public class ContextConfig
        {
            public string ConnectionString { get; set; } = "";
        }

        public RedisConfig Redis { get; set; } = new RedisConfig();
        public class RedisConfig
        {
            public bool Enabled { get; set; } = false;
            public int MaxRdSecond { get; set; }
            public bool EnableLogging { get; set; }
            public int LockMs { get; set; }
            public int SleepMs { get; set; }
            public string SerializerName { get; set; } = "";
            public RedisDatabaseConfig DbConfig { get; set; } = new RedisDatabaseConfig();
            public class RedisDatabaseConfig
            {
                public string Password { get; set; } = "";
                public bool IsSsl { get; set; }
                public string SslHost { get; set; } = "";
                public int SyncTimeout { get; set; }
                public int AsyncTimeout { get; set; }
                public int ConnectionTimeout { get; set; }
                public bool AllowAdmin { get; set; }
                public List<RedisDatabaseEndpointConfig> Endpoints { get; set; } = new List<RedisDatabaseEndpointConfig>();
                public class RedisDatabaseEndpointConfig
                {
                    public string Host { get; set; } = "";
                    public int Port { get; set; }
                }
            }
        }
    }
}
