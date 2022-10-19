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
    /// Interaction logic for OrderItemsAddDialog.xaml
    /// </summary>
    public partial class OrderItemsAddDialog : Window
    {
        public OrderItemsAddDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Globals.dbContext = new FurnishDbConnection();
                LvItemsAvailable.ItemsSource = Globals.dbContext.Products.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }



        private void LvOrderItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LvItemsAvailable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddToOrder_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
