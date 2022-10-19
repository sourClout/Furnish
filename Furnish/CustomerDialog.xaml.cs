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
using Microsoft.Win32;
using System.IO;

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
                disableButtons();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

 
        private void LvCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            disableButtons();

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

            if (custAddDialog.ShowDialog() == true)
            {
                LvCustomers.ItemsSource = Globals.dbContext.Customers.ToList();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Customer chosenCustomer = LvCustomers.SelectedItem as Customer;
            if (chosenCustomer == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this customer?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.dbContext.Customers.Remove(chosenCustomer);
                Globals.dbContext.SaveChanges();
                LvCustomers.ItemsSource = Globals.dbContext.Customers.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveIt = new SaveFileDialog();
            saveIt.Filter = "Text file (*.csv)|*.csv|All files (*.*)|*.*";
            if (saveIt.ShowDialog() != true) return;

            //            List<Trip> tripExport = Globals.DbContext.Travels.ToList();
            List<string> lines = new List<string>();
            foreach (Customer c in LvCustomers.SelectedItems)
            {
                lines.Add($"{c.id};{c.name};{c.email};{c.phone}");
            }
            try
            {
                File.WriteAllLines(saveIt.FileName, lines);
                MessageBox.Show(this, "Export successful!", "Export Status", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                MessageBox.Show(this, "Export failed\n" + ex.Message, "Export Status", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void disableButtons()
        {
            Customer selectedCustomer = LvCustomers.SelectedItem as Customer;
            BtnDelete.IsEnabled = (selectedCustomer != null);
        }

        private void LvCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Customer chosenCustomer = LvCustomers.SelectedItem as Customer;
            if (chosenCustomer == null) return;
            CustomerAddDialog custAddDialog = new CustomerAddDialog(chosenCustomer);
            custAddDialog.Owner = this;

 

            if (custAddDialog.ShowDialog() == true)
            {
                LvCustomers.ItemsSource = Globals.dbContext.Customers.ToList();
            }
        }

        private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx.Text != "")
            {
                var filteredList = Globals.dbContext.Customers.ToList().Where(x => x.name.ToLower().Contains(tbx.Text.ToLower()));
                LvCustomers.ItemsSource = null;
                LvCustomers.ItemsSource = filteredList;
            }
            else 
            {
                LvCustomers.ItemsSource = Globals.dbContext.Customers.ToList();
            }
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
