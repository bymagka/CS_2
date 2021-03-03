using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace ServerASMX
{
    /// <summary>
    /// Summary description for ServerASMX
    /// </summary>
    [WebService(Namespace = "http://serverASMX.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Server : System.Web.Services.WebService
    {
        //адаптер
        static SqlDataAdapter dataAdapter = new SqlDataAdapter();

        static string database = @"data source=(LocalDb)\MSSQLLocalDB;AttachDbFileName = |DataDirectory|\Passwords.mdf;integrated security=True;";


        public Server()
        {

        }
        
        [WebMethod]
        public DataTable GetUsers()
        {
  
            DataTable dt = new DataTable("Users");
            using (SqlConnection sqlConnection = new SqlConnection(database))
            {
                string selectQuery = "SELECT * FROM Users";

                dataAdapter.SelectCommand = new SqlCommand(selectQuery, sqlConnection);

        
                dataAdapter.Fill(dt);

            }

                return dt;
        }
    

        [WebMethod]
        public DataTable GetRoles()
        {
            DataTable dt = new DataTable("roles");
            using (SqlConnection sqlConnection = new SqlConnection(database))
            {
                
                //роли
                string selectQuery = "SELECT * FROM Roles";

                dataAdapter.SelectCommand = new SqlCommand(selectQuery, sqlConnection);

                dataAdapter.Fill(dt);

            }
            return dt;

        }


    } 
}
