using ControlzEx.Standard;
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
using System.Xml.Linq;

namespace Furnish
{
    /// <summary>
    /// Interaction logic for OrderCreateDialog.xaml
    /// </summary>
    public partial class OrderCreateDialog : Window
    {
        public OrderCreateDialog()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!AreOrderInputsValid()) return;
                int employeeId = int.Parse(TbxEmpId.Text);
                int customerId = int.Parse(TbxCustId.Text);
                DateTime dateValue = DateTime.Now;

                string statusValue = ComboStatus.SelectedValue?.ToString();
                StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), statusValue);

                Order newOrder = new Order(employeeId, customerId, dateValue, status);
                Globals.dbContext.Orders.Add(newOrder); // ArgumentException

                Globals.dbContext.SaveChanges(); // SystemException
                this.DialogResult = true; // dismiss the dialog
            }
            
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool AreOrderInputsValid()
        {
            string empId = TbxEmpId.Text;
            if (!Regex.IsMatch(empId, @"^[1-9]\d{0,9}"))
            {
                MessageBox.Show("Id must be a number (integer)", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string cusId = TbxCustId.Text;
            if (!Regex.IsMatch(cusId, @"^[1-9]\d{0,9}"))
            {
                MessageBox.Show("Id must be a number (integer)", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;

        }
    }
}
