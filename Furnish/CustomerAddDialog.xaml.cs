using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Furnish
{
    /// <summary>
    /// Interaction logic for CustomerAddDialog.xaml
    /// </summary>
    public partial class CustomerAddDialog : Window
    {

        Customer currCustomer;

        public CustomerAddDialog(Customer currCustomer = null)
        {
            this.currCustomer = currCustomer;
            InitializeComponent();
            if (currCustomer != null)
            { // load values to be edited
                TbxName.Text = currCustomer.name;
                TbxEmail.Text = currCustomer.email;
                TbxPhone.Text = currCustomer.phone;
                //Converting decimal to string
                btnSave.Content = "Update";
            }
            else
            {
                btnSave.Content = "Add";
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currCustomer != null)
                { // update
                    currCustomer.name = TbxName.Text; // ArgumentException
                    currCustomer.email = TbxEmail.Text; // ArgumentException
                    currCustomer.phone = TbxPhone.Text; // ArgumentException
                }
                else
                { // add
                    Customer newCustomer = new Customer(TbxName.Text, TbxEmail.Text, TbxPhone.Text);
                    Globals.dbContext.Customers.Add(newCustomer); // ArgumentException
                }
                
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



        private bool AreCustomerInputsValid()
        {
            string name = TbxName.Text;
            if (name.Length < 2 || name.Length > 30 || (!Regex.IsMatch(name, @"\([A-Z][a-z]+[ ]*\)+")))
            {
                MessageBox.Show("Name should be 2 to 30 characters, letters only", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string email = TbxEmail.Text;
            if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Wrong email format", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            /*
            string phone = TbxPhone.Text;
            if (!Customer.IsPhoneValid(name, out string errorPhone))
            {
                MessageBox.Show(this, errorPhone, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            */

            return true;

        }



    }
}
