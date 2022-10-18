using System;
using System.Collections.Generic;
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

namespace Furnish
{
    /// <summary>
    /// Interaction logic for ProductsViewDlg.xaml
    /// </summary>
    public partial class ProductsViewDlg : Window
    {
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

        }

        private void LvProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnSaveProducts_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
