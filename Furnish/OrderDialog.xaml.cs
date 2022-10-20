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
    /// Interaction logic for OrderDialog.xaml
    /// </summary>
    public partial class OrderDialog : Window
    {
        public OrderDialog()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Globals.dbContext = new FurnishDbConnection();
                LvOrders.ItemsSource = Globals.dbContext.Orders.ToList();
               // disableButtons();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }



        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            OrderCreateDialog orderCreateDialog = new OrderCreateDialog();
            orderCreateDialog.Owner = this;

            if (orderCreateDialog.ShowDialog() == true)
            {
                LvOrders.ItemsSource = Globals.dbContext.Orders.ToList();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LvOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            Order selectedOrder = LvOrders.SelectedItem as Order;
            if (selectedOrder == null) return;
            OrderItemsAddDialog orderItemsAddDialog = new OrderItemsAddDialog(selectedOrder);
            orderItemsAddDialog.Owner = this;
            if (orderItemsAddDialog.ShowDialog() == true)
            {
                LvOrders.ItemsSource = Globals.dbContext.Orders.ToList();
                
            }


            /*
            if (OrderItemsAddDialog.ShowDialog() == true)
            {
                LvOrders.ItemsSource = Globals.dbContext.Orders.ToList();
            }
            */
        }

        private void LvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LvOrders_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
