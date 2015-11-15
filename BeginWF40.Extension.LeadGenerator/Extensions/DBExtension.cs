using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace LeadGenerator.Extensions
{
    public class DBExtension
    {
        private string _connectionString = "";
        public DBExtension(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string ConnectionString { get { return _connectionString; } }

    }

    public class DBExtensionBehavior : IServiceBehavior
    {
        private string _connectionString;
        public DBExtensionBehavior(string connectionString)
        {
            _connectionString = connectionString;
        }
        public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            WorkflowServiceHost workflowServiceHost
            = serviceHostBase as WorkflowServiceHost;
            if (null != workflowServiceHost)
            {
                DBExtension db = new DBExtension(_connectionString);
                workflowServiceHost.WorkflowExtensions.Add(db);
            }
        }
        public virtual void AddBindingParameters
        (ServiceDescription serviceDescription,
        ServiceHostBase serviceHostBase,
        Collection<ServiceEndpoint> endpoints,
        BindingParameterCollection bindingParameters)
        {
        }
        public virtual void Validate
        (ServiceDescription serviceDescription,
        ServiceHostBase serviceHostBase)
        {
        }
    }

}
