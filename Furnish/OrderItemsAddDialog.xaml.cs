using ControlzEx.Standard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
    /// Interaction logic for OrderItemsAddDialog.xaml
    /// </summary>
    public partial class OrderItemsAddDialog : Window
    {

        Order currOrder;

        public OrderItemsAddDialog(Order currOrder)
        {
            this.currOrder = currOrder;
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Globals.dbContext = new FurnishDbConnection();
                LvItemsAvailable.ItemsSource = Globals.dbContext.Products.ToList();
                LvOrderItems.ItemsSource = Globals.dbContext.OrderItems.ToList().Where(x => x.orderId == currOrder.id);
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
            Product currProduct = LvItemsAvailable.SelectedItem as Product;
            try
            {
                int productId = currProduct.id;
                string productName = currProduct.name;
                int productQty = int.Parse(TbxQty.Text);
                decimal productPrice = currProduct.price * productQty;
                int order_Id = currOrder.id;

                // 
                currProduct.qtyAvailable = currProduct.qtyAvailable - productQty;

                OrderItem newOrderItem = new OrderItem(productId, order_Id, productQty, productPrice);
                Globals.dbContext.OrderItems.Add(newOrderItem); // ArgumentException

                

                Globals.dbContext.SaveChanges(); // SystemException
                LvOrderItems.ItemsSource = Globals.dbContext.OrderItems.ToList().Where(x => x.orderId == currOrder.id);
                LvItemsAvailable.ItemsSource = Globals.dbContext.Products.ToList();
                
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            OrderItem selectedOrderItem = LvOrderItems.SelectedItem as OrderItem;
            if (selectedOrderItem == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this order item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Product prodProduct = (from product in Globals.dbContext.Products where product.id == selectedOrderItem.productId select product).FirstOrDefault<Product>();
                prodProduct.qtyAvailable += selectedOrderItem.qty;
                Globals.dbContext.OrderItems.Remove(selectedOrderItem);
                Globals.dbContext.SaveChanges();
                //LvOrderItems.ItemsSource = Globals.dbContext.OrderItems.ToList();

                LvOrderItems.ItemsSource = Globals.dbContext.OrderItems.ToList().Where(x => x.orderId == currOrder.id);
                LvItemsAvailable.ItemsSource = Globals.dbContext.Products.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
