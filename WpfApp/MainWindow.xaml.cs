using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;


namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> ExistingRoles = new List<string>();

        ObservableCollection<User> usersList = new ObservableCollection<User>();
        ObservableCollection<String> roleList = new ObservableCollection<String>();

  

        public MainWindow()
        {
            InitializeComponent();

            LoadData();
          
        }

        private void LoadData()
        {
            ServerASMX.ServerSoapClient serviceClient = new ServerASMX.ServerSoapClient();
            var dt = serviceClient.GetUsers();
          
            var roles = serviceClient.GetRoles();
            ////получаем пользователей
            foreach (DataRow row in dt.Rows)
            {
                User existingUser = new User { UserName = row["UserName"].ToString(), UserPassword = row["Password"].ToString(), Role = row["Role"].ToString(), Id = int.Parse(row["Id"].ToString()) };
                usersList.Add(existingUser);


            }




            foreach (DataRow row in roles?.Rows)
            {
                roleList.Add(row["Name"].ToString());
                ExistingRoles.Add(row["Name"].ToString());
            }

            //}
            userDataGrid.ItemsSource = usersList;




        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window addForm = new AddForm(usersList,roleList);
            addForm.ShowDialog();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {


            //using (SqlConnection sqlConnection = new SqlConnection(database))
            //{
            //    //роли
            //    string updateQuery = @"INSERT INTO Roles (Name) VALUES (@Name)";

            //    SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
            //    updateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            //    DataTable dtRoles = new DataTable();
            //    dtRoles.Columns.Add("Name");

            //    foreach (string role in roleList)
            //    {
            //        if(ExistingRoles.Find(x => x == role) == null)
            //        {
                      
            //            dataAdapter.InsertCommand = updateCommand;
            //            DataRow dr = dtRoles.NewRow();
            //            dr["Name"] = role;

            //            dtRoles.Rows.Add(dr);

            //            dataAdapter.Update(dtRoles);

            //            ExistingRoles.Add(role);
            //        }
                    

            //    }

            //    //users
            //    //insert
            //    string insertQueryUsers = @"INSERT INTO Users (UserName,Password,Role) VALUES (@UserName,@Password,@Role); SET @ID = @@IDENTITY";

            //    SqlCommand insertUsers = new SqlCommand(insertQueryUsers, sqlConnection);
            //    insertUsers.Parameters.Add("@UserName", SqlDbType.NVarChar, -1, "UserName");
            //    insertUsers.Parameters.Add("@Password", SqlDbType.NVarChar, -1, "Password");
            //    insertUsers.Parameters.Add("@Role", SqlDbType.NVarChar, -1, "Role");
            //    insertUsers.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

            //    DataTable dtUsers = new DataTable();
            //    dtUsers.Columns.Add("UserName");
            //    dtUsers.Columns.Add("Password");
            //    dtUsers.Columns.Add("Role");
            //    dtUsers.Columns.Add("Id");

            //    //update
            //    string updateQueryUsers = @"UPDATE Users SET UserName = @UserName, Password = @Password, Role = @Role WHERE Id = @Id";

            //    SqlCommand updateUsers = new SqlCommand(updateQueryUsers, sqlConnection);
            //    updateUsers.Parameters.Add("@UserName", SqlDbType.NVarChar, -1, "UserName");
            //    updateUsers.Parameters.Add("@Password", SqlDbType.NVarChar, -1, "Password");
            //    updateUsers.Parameters.Add("@Role", SqlDbType.NVarChar, -1, "Role");
            //    updateUsers.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

            //    DataTable dtUpdateUsers = new DataTable();

            //    string selectQuery = "SELECT * FROM Users WHERE Id = @Id";
             

            //    foreach (User user in usersList)
            //    { 
            //        if (user.Id == 0)
            //        {
            //            DataRow dr = dtUsers.NewRow();
            //            dr["UserName"] = user.UserName;
            //            dr["Password"] = user.UserPassword;
            //            dr["Role"] = user.Role;
            //            dr["Id"] = user.Id;

            //            dataAdapter.InsertCommand = insertUsers;
                   
            //            dtUsers.Rows.Add(dr);

            //            dataAdapter.Update(dtUsers);
            //        }
            //        else
            //        {

            //            SqlCommand selectCommand = new SqlCommand(selectQuery,sqlConnection);
            //            selectCommand.Parameters.AddWithValue("@Id", user.Id);
            //            dataAdapter.SelectCommand = selectCommand;

            //            dataAdapter.Fill(dtUpdateUsers);
                        
            //            dataAdapter.UpdateCommand = updateUsers;

            //           if(dtUpdateUsers.Rows.Count > 0)
            //            {
            //                DataRow elementRow = dtUpdateUsers.Rows[0];

            //                elementRow["UserName"] = user.UserName;
            //                elementRow["Password"] = user.UserPassword;
            //                elementRow["Role"] = user.Role;
            //                dataAdapter.Update(dtUpdateUsers);
            //            }

                        
            //        }

            //    }



            //}
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            Window addRoleForm = new AddRole(roleList);
            addRoleForm.ShowDialog();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var activeUser = userDataGrid.SelectedValue;

            if (activeUser == null)
                MessageBox.Show("You didn't select an item");
            else
            {
                Window editForm = new EditForm((User)activeUser, roleList);
                editForm.ShowDialog();
            }

        }
    }
}
