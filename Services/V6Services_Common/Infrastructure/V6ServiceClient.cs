using System;
using System.Globalization;
using System.Reflection;
using System.ServiceModel;

using V6Soft.Common.Utils;


namespace V6Soft.Services.Common.Infrastructure
{
    /// <summary>
    /// A generic class that supports initialization and connection to a WCF service interface.
    /// The client returns a client connection through the Client property. The client is connected and ready to use.
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    /// <typeparam name="TChannel"></typeparam>
    public class V6ServiceClient<TClient, TChannel> 
        where TClient : System.ServiceModel.ClientBase<TChannel>, new()
        where TChannel : class
    {
        private TClient m_Client;
        private string m_EndpointConfigurationName;
        private string m_AddressFormula;

        /// <summary>
        /// Creates a new instance of the MtpServiceClient class with specified arguments.
        /// </summary>
        /// <param name="serviceTypeId">Id of service type to resolve.</param>
        /// <param name="realmId">Acts like "namespace", used for diffirentate services with the same name.</param>
        /// <param name="endpointName">Name of endpoint configuration to use; e.g. 'NetTcpBinding_ConfigurationService'</param>
        /// <param name="addressFormula">Address formulae to use; e.g. 'net.tcp://{0}:8000/ConfigurationService'. Where {0} always implies server name.</param>
        public V6ServiceClient(string serviceTypeId, string realmId, string endpointName, 
            string addressFormula)
        {
            Guard.ArgumentNotNull(serviceTypeId, "serviceTypeId");
            Guard.ArgumentNotNull(realmId, "realmId");
            Guard.ArgumentNotNull(endpointName, "endpointName");
            Guard.ArgumentNotNull(addressFormula, "addressFormula");

            m_EndpointConfigurationName = endpointName;
            m_AddressFormula = addressFormula;
            //TODO: Service Locator provides a list of addresses of servers where this service type is deployed.
            //Locator = new ServiceLocator(serviceTypeId, realmId);
        }

        /// <summary>
        /// Creates a new instance of the MtpServiceClient class with specified arguments.
        /// </summary>
        /// <param name="serviceTypeId">Id of service type to resolve. LocateServiceType class contains constants for service type ids.</param>
        /// <param name="endpointName">Name of endpoint configuration to use; e.g. 'NetTcpBinding_ConfigurationService'</param>
        /// <param name="addressFormula">Address formulae to use; e.g. 'net.tcp://{0}:8000/ConfigurationService'. Where {0} always implies server name.</param>
        public V6ServiceClient(string serviceTypeId, string endpointName, string addressFormula)
        {
            Guard.ArgumentNotNull(serviceTypeId, "serviceTypeId");
            Guard.ArgumentNotNull(endpointName, "endpointName");
            Guard.ArgumentNotNull(addressFormula, "addressFormula");

            m_EndpointConfigurationName = endpointName;
            m_AddressFormula = addressFormula;            
            //Locator = new ServiceLocator(serviceTypeId);
        }

        /// <summary>
        /// Gets the ServiceLocator used to resolve the service location.
        /// </summary>
        //public ServiceLocator Locator { get; private set; }

        /// <summary>
        /// Gets a proxy client instance. Will return null if a connection could not be established.
        /// </summary>
        public TClient Client 
        {
            get
            {
                if (m_Client == null || m_Client.State != CommunicationState.Opened)
                {
                    CreateClient();
                }
                return m_Client;
            }
        }

        private void CreateClient()
        {
            if (m_Client != null)
            {
                try
                {
                    if (m_Client.State == CommunicationState.Faulted)
                    {
                        m_Client.Abort();
                    }
                    else
                    {
                        m_Client.Close();    
                    }
                }
                catch (Exception closeEx)
                {
                    AppContext.Logger.LogException("Exception when closing WCF client", closeEx);
                }
                finally
                {
                    m_Client = null;
                }
            }
            m_Client = null;

            try
            {
                string clientAddress = string.Format(CultureInfo.InvariantCulture, m_AddressFormula, "");
                Type clientType = typeof(TClient);
                ConstructorInfo classConstructor = clientType.GetConstructor(new Type[] { typeof(string), typeof(string) });
                m_Client = (TClient)classConstructor.Invoke(new object[] { m_EndpointConfigurationName, clientAddress });
                m_Client.Open();
                if (m_Client.State == CommunicationState.Opened)
                {
                    return;
                }
                if (m_Client.State == CommunicationState.Faulted)
                {
                    m_Client.Abort();
                }
                else
                {
                    m_Client.Close();
                }
            }
            catch (Exception openEx)
            {
                AppContext.Logger.LogException("Could not open connection to service", openEx);
                if (m_Client != null)
                {
                    try
                    {
                        if (m_Client.State == CommunicationState.Faulted)
                        {
                            m_Client.Abort();
                        }
                        m_Client.Close();
                    }
                    catch
                    { }
                }
            }
        }

        
    }
}
