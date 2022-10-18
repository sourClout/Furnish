
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

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Globals.dbContext = new FurnishDbConnection();
                
        //    }
        //    catch (SystemException ex)
        //    {
        //        MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        Environment.Exit(1);
        //    }
        //}

    
    }

    

    private void BtnCustomers_Click(object sender, RoutedEventArgs e)
    {
            CustomerDialog dialog = new CustomerDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            Login dialog = new Login(); 
            dialog.Owner = this;
  
           
            if (dialog.ShowDialog() == true)
                TbxUserName.Text = dialog.Name;
           
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsViewDlg dialog = new ProductsViewDlg();
            dialog.Owner = this;
            dialog.ShowDialog();
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

