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
using System.Windows.Shapes;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Xml.Linq;

namespace Furnish
{
    /// <summary>
    /// Interaction logic for CustomerDialog.xaml
    /// </summary>
    public partial class CustomerDialog : Window
    {
        public CustomerDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Globals.dbContext = new FurnishDbConnection();
                LvCustomers.ItemsSource = Globals.dbContext.Customers.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        
        private void LvCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer selectedCustomer = LvCustomers.SelectedItem as Customer;
            BtnUpdate.IsEnabled = (selectedCustomer != null);
            BtnDelete.IsEnabled = (selectedCustomer != null);

            /*
            if (selectedCustomer == null)
            {
                ResetFields();
            }
            else
            {
                TbxName.Text = selectedCustomer.name;
                TbxEmail.Text = selectedCustomer.email;
                TbxPhone.Text = selectedCustomer.phone;
            }
            */
        }
        
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddDialog custAddDialog = new CustomerAddDialog();
            custAddDialog.Owner = this;
            custAddDialog.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        /*
        private void ResetFields()
        {
            TbxName.Text = "";
            TbxEmail.Text = "";
            TbxPhone.Text = "";
        }
        */
    }
}
