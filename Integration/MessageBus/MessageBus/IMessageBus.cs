using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(object message, string topic_queue_Name);
        Task PublishTopicMessage(object message, string topic_queue_Name, string messageType);
    }
}
