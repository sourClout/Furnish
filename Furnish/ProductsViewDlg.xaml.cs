using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Xml.Linq;

namespace Furnish
{
    /// <summary>
    /// Interaction logic for ProductsViewDlg.xaml
    /// </summary>
    public partial class ProductsViewDlg : Window
    {
        const string DataFileName = @"..\..\..\products.csv";
        public ProductsViewDlg()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Globals.dbContext = new FurnishDbConnection();
                LvProd.ItemsSource = Globals.dbContext.Products.ToList();
                TbkStatus.Text = "Data loaded";
                disableButtons();

            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

       

        private void BtnAddProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsAddEditDlg dlg = new ProductsAddEditDlg();
            dlg.Owner = this;
            if (dlg.ShowDialog() == true)
            {
                LvProd.ItemsSource = Globals.dbContext.Products.ToList(); // equivalent of SELECT * FROM people
                TbkStatus.Text = "Product added";
            }
        }

        private void LvProd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product currSelectedProduct = LvProd.SelectedItem as Product;
            if (currSelectedProduct == null) return; // nothing selected
            ProductsAddEditDlg dlg = new ProductsAddEditDlg(currSelectedProduct);
            dlg.Owner = this;
            if (dlg.ShowDialog() == true)
            {
                LvProd.ItemsSource = Globals.dbContext.Products.ToList();
                TbkStatus.Text = "Product updated";
            }

        }

        private void BtnDeleteProducts_Click(object sender, RoutedEventArgs e)
        {
            Product currSelectedProduct = LvProd.SelectedItem as Product;
            if (currSelectedProduct == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this product?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.dbContext.Products.Remove(currSelectedProduct);
                Globals.dbContext.SaveChanges();
                LvProd.ItemsSource = Globals.dbContext.Products.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LvProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Product currSelectedProduct = LvProd.SelectedItem as Product;
            //BtnDeleteProducts.IsEnabled = (currSelectedProduct != null);
            //BtnSaveProducts.IsEnabled = (currSelectedProduct != null);
            disableButtons();
        }

        private void BtnSaveProducts_Click(object sender, RoutedEventArgs e)
        {
           
            SaveDataToFile();

        }

        private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx.Text != "")
            {
                var filteredList = Globals.dbContext.Products.ToList().Where(x => x.name.ToLower().Contains(tbx.Text.ToLower()));
                LvProd.ItemsSource = null;
                LvProd.ItemsSource = filteredList;
            }
            else
            {
                LvProd.ItemsSource = Globals.dbContext.Products.ToList();
            }
        }


        private void disableButtons()
        {
            Product currSelectedProduct = LvProd.SelectedItem as Product;
            BtnDeleteProducts.IsEnabled = (currSelectedProduct != null);
            BtnSaveProducts.IsEnabled = (currSelectedProduct != null);
        }

        private void SaveDataToFile()
        {

            List<string> lines = new List<string>();
            foreach (Product p in LvProd.SelectedItems)
            {
                lines.Add($"{p.id};{p.name};{p.description};{p.price}; {p.qtyAvailable} ");
            }
            try
            {
                File.WriteAllLines(DataFileName, lines);
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                MessageBox.Show(this, "Error reading from file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBox.Show(this, "Data saved to file", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
