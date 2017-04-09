using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using SpringCloud.Connector.Common;

namespace SpringCloud.Connector.Redis
{
    public class RedisOption : AbstractOption
    {
        public const string DefaultHost = "localhost";
        public const int DefaultPort = 6379;
        public static string DefaultEndPoints = DefaultHost + ":" + DefaultPort;

        private const string RedisClientSectionPrefix = "redis:client";

        // You can use this instead of configuring each option seperately
        // If a connection string is provided, the string will be used and 
        // the optioons above will be ignored
        public string ConnectionString { get; set; }

        // Configure either a single Host/Port or optionaly provide 
        // a list of endpoints (ie. host1:port1,host2:port2)
        public string Host { get; set; } = DefaultHost;

        public int Port { get; set; } = DefaultPort;

        public string EndPoints { get; set; }

        public string Password { get; set; }

        public bool AllowAdmin { get; set; } = false;

        public string ClientName { get; set; }

        public int ConnectRetry { get; set; }

        public int ConnectTimeout { get; set; }

        public bool AbortOnConnectFail { get; set; } = true;

        public int KeepAlive { get; set; }

        public bool ResolveDns { get; set; } = false;

        public string ServiceName { get; set; }

        public bool Ssl { get; set; } = false;

        public string SslHost { get; set; }

        public string TieBreaker { get; set; }

        public int WriteBuffer { get; set; }

        // This configuration option specfic to https://github.com/aspnet/Caching
        public string InstanceId { get; set; }

        public RedisOption(IConfiguration config) :
            base(',', DefaultSeparator)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            var section = config.GetSection(RedisClientSectionPrefix);
            section.Bind(this);
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                return ConnectionString;
            }

            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(EndPoints))
            {

                string endpoints = EndPoints.Trim();
                sb.Append(endpoints);
                sb.Append(',');
            }
            else
            {
                sb.Append(Host + ":" + Port);
                sb.Append(',');
            }

            AddKeyValue(sb, "password", Password);
            AddKeyValue(sb, "allowAdmin", AllowAdmin);
            AddKeyValue(sb, "name", ClientName);

            if (ConnectRetry > 0)
            {
                AddKeyValue(sb, "connectRetry", ConnectRetry);
            }

            if (ConnectTimeout > 0)
            {
                AddKeyValue(sb, "connectTimeout", ConnectTimeout);
            }

            AddKeyValue(sb, "abortConnect", AbortOnConnectFail);

            if (KeepAlive > 0)
            {
                AddKeyValue(sb, "keepAlive", KeepAlive);
            }

            AddKeyValue(sb, "resolveDns", ResolveDns);
            AddKeyValue(sb, "serviceName", ServiceName);
            AddKeyValue(sb, "ssl", Ssl);
            AddKeyValue(sb, "sslHost", SslHost);
            AddKeyValue(sb, "tiebreaker", TieBreaker);

            if (WriteBuffer > 0)
            {
                AddKeyValue(sb, "writeBuffer", WriteBuffer);
            }

            // Trim ending ','
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}