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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StreamReader sr = null;
        List<User> usersList = null;
        string database = String.Empty;
        public MainWindow()
        {
            InitializeComponent();
            userDataGrid.ItemsSource = usersList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            usersList = new List<User>();

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

                while (!sr.EndOfStream)
                {
                    string[] user = sr.ReadLine().Split(';');
                   
                    if(user.Length > 1)
                         usersList.Add(new User { UserName = user[0], UserPassword = user[1] });
                }
                userDataGrid.ItemsSource = usersList;
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            sr?.Close();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            usersList.Add(new User { UserName = tbUserName.Text, UserPassword = tbUserPassword.Text });
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(database,false);

            foreach (User item in usersList)
            {
                sw.WriteLine($"{item.UserName};{item.UserPassword}");
            }

            sw.Close();
        }
    }
}
