using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;


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
            //ResetFields();

        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (currProduct != null)
                { // update
                    try
                    {
                        if (!AreProductInputsValid()) return;
                        currProduct.name = NameInput.Text; // ArgumentException
                        currProduct.description = DescriptionInput.Text; // ArgumentException
                        // convert string to decimal
                        currProduct.price = decimal.Parse(PriceInput.Text);
                        //currProduct.price = PriceInput.Text; // ArgumentException
                        currProduct.qtyAvailable = (int)QuantitySlider.Value; // ArgumentException
                        //currProduct.imageUrl = ImageInput.Text;
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                { // add
                    try
                    {
                        //FIXME: Product has 5 fields due to IMAGE --> either add image or create new 4 field constructor
                        if (!AreProductInputsValid()) return;
                        Product newProduct = new Product(NameInput.Text, DescriptionInput.Text, decimal.Parse(PriceInput.Text), (int)QuantitySlider.Value);
                        Globals.dbContext.Products.Add(newProduct); // ArgumentException
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                Globals.dbContext.SaveChanges(); // SystemException
                this.DialogResult = true; // dismiss the dialog
                }
            //catch (ArgumentException ex)
            //{
            //    MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            if (dlg.ShowDialog() == true)
            {
                FileStream imageStream = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);

                byte[] iBytes = new byte[imageStream.Length + 1];
            }
            /*
           
            /*
            using (FilesEntities entities = new FilesEntities())
            {
                tblFile file = new tblFile
                {
                    Name = Path.GetFileName(fuUploadFile.PostedFile.FileName),
                    ContentType = fuUploadFile.PostedFile.ContentType,
                    Data = fuUploadFile.FileBytes,
                };
                entities.AddTotblFiles(file);
                entities.SaveChanges();
            }
            */


            /*
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Images files|*.bmp;*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openDialog.FileName);
                imagePic.Source = new BitmapImage(fileUri);
            }
            */
        }



        private bool AreProductInputsValid()
        {
            /*
            string name = NameInput.Text;
            if (name.Length < 2 || name.Length > 30 || (!Regex.IsMatch(name, @"([A-Z][a-z]+[ ]*)+")))
            {
                MessageBox.Show("Product name should be 2 to 30 characters, letters only", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            string email = TbxEmail.Text;
            if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Wrong email format", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string phone = TbxPhone.Text;
            if (!Regex.IsMatch(phone, @"^[1-9]\d{2}-\d{3}-\d{4}"))
            {
                MessageBox.Show("Phone number format must be 111-111-1111 ", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            */


            return true;

        }



        /*
        private void ResetFields()
        {
            NameInput.Text = "";
            DescriptionInput.Text = "";
            PriceInput.Text = "";
            QuantitySlider.Value = QuantitySlider.SelectionStart;
            // IMAGE

        }
        */


    }
}
