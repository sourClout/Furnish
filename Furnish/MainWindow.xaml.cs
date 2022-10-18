
using MahApps.Metro.Controls.Dialogs;
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


namespace Furnish
{    /// <summary>    /// Interaction logic for MainWindow.xaml    /// </summary>    
    public partial class MainWindow : Window    {        
        
    public MainWindow()        {            InitializeComponent();        }

    private void BtnOrders_Click(object sender, RoutedEventArgs e)
    {

    }

    private void BtnProducts_Click(object sender, RoutedEventArgs e)
    {

    }

    private void BtnCustomers_Click(object sender, RoutedEventArgs e)
    {

    }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            Login dialog = new Login(); 
            dialog.Owner = this;
  
           
            if (dialog.ShowDialog() == true)
                TbxUserName.Text = dialog.Name;
           
        }

    private void BtnRegister_Click(object sender, RoutedEventArgs e)
    {
            Register dialog = new Register();
            dialog.Owner = this;
            dialog.ShowDialog();
        }

        private void TbxUserName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

