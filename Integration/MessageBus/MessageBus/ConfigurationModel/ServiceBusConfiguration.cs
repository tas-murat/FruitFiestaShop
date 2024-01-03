namespace MessageBus.ConfigurationModel
{
    public class ServiceBusConfiguration : IServiceBusConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
