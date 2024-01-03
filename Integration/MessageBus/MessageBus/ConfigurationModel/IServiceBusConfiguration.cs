using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBus.ConfigurationModel
{
    public interface IServiceBusConfiguration
    {
        string ConnectionString { get; }
    }
}
