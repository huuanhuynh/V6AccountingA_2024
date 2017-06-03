using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;


namespace V6Soft.Common.DeInWcf
{
    public class DeInInstanceContextInitializer : IInstanceContextInitializer
    {
        public void Initialize(InstanceContext instanceContext, Message message)
        {
            instanceContext.Extensions.Add(new DeInInstanceContextExtension());
        }
    }
}
