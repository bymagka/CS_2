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
        ServerASMX.ServerSoapClient serviceClient = new ServerASMX.ServerSoapClient();


        public MainWindow()
        {
            InitializeComponent();

            LoadData();
          
        }

        private void LoadData()
        {
            usersList.Clear();
            roleList.Clear();

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
            var addingRoles = roleList.Select(x => x).Where(x => ExistingRoles.Find(y => y == x) == null);
            serviceClient.AddRoles(addingRoles.ToArray());

            foreach(var item in addingRoles)
            {
                ExistingRoles.Add(item);
            }

            //    }

            //    //users
            DataTable dtUsers = new DataTable("AddRoles");
            dtUsers.Columns.Add("UserName");
            dtUsers.Columns.Add("Password");
            dtUsers.Columns.Add("Role");
            dtUsers.Columns.Add("Id");

            DataTable dtUpdateUsers = dtUsers.Clone();
            dtUpdateUsers.TableName = "UpdateUsers";

            foreach (User user in usersList)
            {
                if (user.Id == 0)
                {
                    DataRow dr = dtUsers.NewRow();
                    dr["UserName"] = user.UserName;
                    dr["Password"] = user.UserPassword;
                    dr["Role"] = user.Role;
                    dr["Id"] = user.Id;

                    dtUsers.Rows.Add(dr);

                }
                else
                {

                    DataRow dr = dtUpdateUsers.NewRow();
                    dr["UserName"] = user.UserName;
                    dr["Password"] = user.UserPassword;
                    dr["Role"] = user.Role;
                    dr["Id"] = user.Id;

                    dtUpdateUsers.Rows.Add(dr);

                }
            }

            serviceClient.AddUsers(dtUsers);
            serviceClient.UpdateUsers(dtUpdateUsers);
            LoadData();
        
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
