using Microsoft.Win32;
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
    /// Interaction logic for ProductsAddEditDlg.xaml
    /// </summary>
    public partial class ProductsAddEditDlg : Window
    {
        Product currProduct;

        public ProductsAddEditDlg(Product currProduct = null)
        {
            this.currProduct = currProduct;
            InitializeComponent();
            if (currProduct != null)
            { // load values to be edited
                NameInput.Text = currProduct.name;
                DescriptionInput.Text = currProduct.description;
                //Converting decimal to string 
                PriceInput.Text = currProduct.price.ToString();
                QuantitySlider.Value = currProduct.qtyAvailable;
                
                //ImageInput.Text = currProduct.imageUrl;
                BtSave.Content = "update";
            }
            else
            {
                BtSave.Content = "Add";
            }

        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            

            try
                
            {
                

                // FIXME: due date may be null
                if (currProduct != null)
                { // update
                   
                    currProduct.name = NameInput.Text; // ArgumentException
                    currProduct.description = DescriptionInput.Text; // ArgumentException
                    // Must convert string to decimal
                    currProduct.price = decimal.Parse(PriceInput.Text);
                    //currProduct.price = PriceInput.Text; // ArgumentException
                    currProduct.qtyAvailable = (int)QuantitySlider.Value; // ArgumentException
                    //currProduct.imageUrl = ImageInput.Text;
                    
                }
                else
                { // add
                  //FIXME: Product has 5 fields due to IMAGE --> either add image or create new 4 field constructor

                    Product newProduct = new Product(NameInput.Text, DescriptionInput.Text, decimal.Parse(PriceInput.Text), (int)QuantitySlider.Value);
                    Globals.dbContext.Products.Add(newProduct); // ArgumentException
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Images files|*.bmp;*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog()==true)
            {
                Uri fileUri = new Uri(openDialog.FileName);
                imagePic.Source = new BitmapImage(fileUri);
            }
        }
    }
}
