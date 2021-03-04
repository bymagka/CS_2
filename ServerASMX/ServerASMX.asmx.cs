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

        [WebMethod]
        public void AddRoles(string[] rolesArray)
        {
            using (SqlConnection sqlConnection = new SqlConnection(database))
            {
                //роли
                string updateQuery = @"INSERT INTO Roles (Name) VALUES (@Name)";

                SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                updateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
                DataTable dtRoles = new DataTable();
                dtRoles.Columns.Add("Name");

                foreach (string role in rolesArray)
                {
                        dataAdapter.InsertCommand = updateCommand;
                        DataRow dr = dtRoles.NewRow();
                        dr["Name"] = role;

                        dtRoles.Rows.Add(dr);

                        dataAdapter.Update(dtRoles);
                }

            }
        }

        [WebMethod]
        public void AddUsers(DataTable dtUsers)
        {
            using (SqlConnection sqlConnection = new SqlConnection(database))
            {
                //insert
                string insertQueryUsers = @"INSERT INTO Users (UserName,Password,Role) VALUES (@UserName,@Password,@Role); SET @ID = @@IDENTITY";

                SqlCommand insertUsers = new SqlCommand(insertQueryUsers, sqlConnection);
                insertUsers.Parameters.Add("@UserName", SqlDbType.NVarChar, -1, "UserName");
                insertUsers.Parameters.Add("@Password", SqlDbType.NVarChar, -1, "Password");
                insertUsers.Parameters.Add("@Role", SqlDbType.NVarChar, -1, "Role");
                insertUsers.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

                dataAdapter.InsertCommand = insertUsers;

                dataAdapter.Update(dtUsers);
            }
        }


        [WebMethod]
        public void UpdateUsers(DataTable dtUsers)
        {
            using (SqlConnection sqlConnection = new SqlConnection(database))
            {

                //insert
                string updateQueryUsers = @"UPDATE Users SET UserName = @UserName, Password = @Password, Role = @Role WHERE Id = @Id";

                SqlCommand updateUsers = new SqlCommand(updateQueryUsers, sqlConnection);
                updateUsers.Parameters.Add("@UserName", SqlDbType.NVarChar, -1, "UserName");
                updateUsers.Parameters.Add("@Password", SqlDbType.NVarChar, -1, "Password");
                updateUsers.Parameters.Add("@Role", SqlDbType.NVarChar, -1, "Role");
                updateUsers.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

                dataAdapter.InsertCommand = updateUsers;

                dataAdapter.Update(dtUsers);
            }
        }

    }
}
