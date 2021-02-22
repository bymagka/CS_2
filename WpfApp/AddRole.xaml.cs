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
    /// Interaction logic for AddRole.xaml
    /// </summary>
    public partial class AddRole : Window
    {
        ObservableCollection<String> roleList;

        public AddRole(ObservableCollection<String> roleList)
        {
            this.roleList = roleList;

            InitializeComponent();
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            roleList.Add(tbName.Text);
            Close();
        }
    }
}
