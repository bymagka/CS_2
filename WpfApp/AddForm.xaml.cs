using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddForm : Window
    {
        ObservableCollection<User> usersList;

        public AddForm(ObservableCollection<User> usersList, ObservableCollection<String> roleList)
        {
 
            InitializeComponent();

            this.usersList = usersList;
            cbRole.ItemsSource = roleList;

            if (roleList.Count > 0) cbRole.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            usersList.Add(new User { UserName = tbUserName.Text, UserPassword = tbUserPassword.Text, Role = cbRole.SelectedItem.ToString()});
            Close();
        }
    }
}
