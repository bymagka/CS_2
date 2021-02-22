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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StreamReader sr = null;
        ObservableCollection<User> usersList = null;
        ObservableCollection<String> roleList = null;

        string database = String.Empty;

        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            usersList = new ObservableCollection<User>();
            roleList = new ObservableCollection<String>();

            OpenFileDialog opf = new OpenFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FilterIndex = 1
            };

            opf.ShowDialog();

            if (opf.FileName != String.Empty)
            {
                database = opf.FileName;
                sr = new StreamReader(opf.FileName);
                string line = String.Empty;

                //users
                while (true)
                {
                    if (sr.EndOfStream) break;

                    line = sr.ReadLine();

                    if (line.Equals("ROLES")) break;

                    string[] user = line.Split(';');
                   
                    if(user.Length > 2)
                         usersList.Add(new User { UserName = user[0], UserPassword = user[1], Role = user[2] });
                }

                //roles
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();

                    roleList.Add(line);
                }

                userDataGrid.ItemsSource = usersList;
                sr.Close();
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            sr?.Close();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window addForm = new AddForm(usersList,roleList);
            addForm.ShowDialog();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(database,false);

            foreach (User item in usersList)
            {
                sw.WriteLine($"{item.UserName};{item.UserPassword};{item.Role}");
            }

            sw.WriteLine("ROLES");

            foreach (string item in roleList)
            {
                sw.WriteLine(item);
            }

            sw.Close();
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
