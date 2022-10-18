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

namespace Furnish
{
    /// <summary>
    /// Interaction logic for CustomerAddDialog.xaml
    /// </summary>
    public partial class CustomerAddDialog : Window
    {

        Customer currCustomer;

        public CustomerAddDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer newCustomer = new Customer(TbxName.Text, TbxEmail.Text, TbxPhone.Text);
                Globals.dbContext.Customers.Add(newCustomer); // ArgumentException
                /*
                // FIXME: due date may be null
                if (currCustomer != null)
                { // update
                    currCustomer.name = TbxName.Text; // ArgumentException
                    currCustomer.email = TbxEmail.Text; // ArgumentException
                    currCustomer.phone = TbxPhone.Text; // ArgumentException
                }
                else
                { // add
                    Customer newCustomer = new Customer(TbxName.Text, TbxEmail.Text, TbxPhone.Text);
                    Globals.DbContext.Customers.Add(newCustomer); // ArgumentException
                }
                */
                Globals.dbContext.SaveChanges(); // SystemException
                this.DialogResult = true; // dismiss the dialog
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }
    }
}
