namespace ApiDemo.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string connectionString
        {
            get { return $"mongodb://{Host}:{Port}"; }
        }

    }
}
