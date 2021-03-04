using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServerWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        static ServerASMX.ServerSoapClient serviceClient = new ServerASMX.ServerSoapClient();


        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public DataTable GetRoles()
        {
            return serviceClient.GetRoles();
        }

        public DataTable GetUsers()
        {
            return serviceClient.GetUsers();
        }

        public void UpdateUsers(DataTable dtUsers)
        {
            serviceClient.UpdateUsers(dtUsers);
        }

        public void AddRoles(string[] rolesArray)
        {
            serviceClient.AddRoles(rolesArray);
        }

        public void AddUsers(DataTable dtUsers)
        {
            serviceClient.AddUsers(dtUsers);
        }
    }
}
