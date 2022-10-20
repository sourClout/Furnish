using Microsoft.Win32;
using System;
using System.IO;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using Azure;

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



        SqlConnection con = new SqlConnection("Data Source=fsd04.database.windows.net;Initial Catalog=Furnish;User ID=myadmin;Password=Ouradmin03");
        string imgLocation = "";
        SqlCommand cmd;



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Images files|*.bmp;*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                imgLocation = openDialog.FileName.ToString();
                Uri fileUri = new Uri(openDialog.FileName);
                pictureBox1.Source = new BitmapImage(fileUri);
            }
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {



            try
            {
                byte[] images = null;
                FileStream Stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(Stream);
                images = brs.ReadBytes((int)Stream.Length);



                if (currProduct != null)
                {
                    
                        currProduct.name = NameInput.Text; // ArgumentException
                        currProduct.description = DescriptionInput.Text; // ArgumentException
                                                                         
                        currProduct.price = decimal.Parse(PriceInput.Text); // ArgumentException
                        //currProduct.price = PriceInput.Text; 
                        currProduct.qtyAvailable = (int)QuantitySlider.Value; // ArgumentException
                        currProduct.image = images;
                        Globals.dbContext.SaveChanges();
                    
                    
                    
                }
                else
                { // add
                  //FIXME: Product has 5 fields due to IMAGE --> either add image or create new 4 field constructor
                    con.Open();
                    string sqlQuery = "Insert into Products (name,description,price,qtyAvailable,image) values('" + NameInput.Text + "','" + DescriptionInput.Text + "','" + decimal.Parse(PriceInput.Text) + "','" + (int)QuantitySlider.Value + "', @images)";



                    cmd = new SqlCommand(sqlQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@images", images));
                    int N = cmd.ExecuteNonQuery();
                    con.Close();



                    //Product newProduct = new Product(NameInput.Text, DescriptionInput.Text, decimal.Parse(PriceInput.Text), (int)QuantitySlider.Value, @images);
                    /* Globals.dbContext.Products.Add(newProduct);*/ // ArgumentException
                }
                // SystemException
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

        /*
        private bool AreCustomerInputsValid()
        {
            string name = NameInput.Text;
            if (name.Length < 2 || name.Length > 30 || (!Regex.IsMatch(name, @"([A-Z][a-z]+[ ]*)+")))
            {
                MessageBox.Show("Name should be 2 to 30 characters, letters only", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            string description = DescriptionInput.Text;
            if (description.Length < 1 || description.Length > 100)
            {
                MessageBox.Show("Description must be between 1 - 100 characrters long", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            decimal price = decimal.Parse(PriceInput.Text);
            if (price < 0 )
            
                {
                MessageBox.Show("Price should be greater than 0", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;

        }
        */




    }
}


