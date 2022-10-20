
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Furnish
{    /// <summary>    /// Interaction logic for MainWindow.xaml    /// </summary>    
    public partial class MainWindow : Window    {        
        
    public MainWindow()
        { 
            InitializeComponent();
            try
            {
                Globals.dbContext = new FurnishDbConnection();
                
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }

            
        }

        private void btnMinimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                
                this.WindowState = WindowState.Normal;

                btnMaximize.Content = "☐";
            }
            else
            {
               
                this.WindowState = WindowState.Maximized;

                btnMaximize.Content = "[-]";
            }
        }


        private void BtnOrders_Click(object sender, RoutedEventArgs e)
    {
            OrderDialog dialog = new OrderDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
    }

 

    private void BtnCustomers_Click(object sender, RoutedEventArgs e)
    {
            CustomerDialog dialog = new CustomerDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
        }

        static Employee Emp = null;

        public static List<Employee> GetList()
        {   
            List<Employee> Emplist = new List<Employee>();

            string _name = App.Current.Properties[1].ToString();
     
            Emp = (from employee in Globals.dbContext.Employees where employee.name == _name select employee).FirstOrDefault<Employee>();

            if(Emp == null)
            {
                Emplist= null;
            }
            else if (Emp.role == RoleEnum.User)
            {
                Emplist.Add(Emp);

            }
            else
            {
                Emplist = Globals.dbContext.Employees.ToList();
            }

            return Emplist;
        }



        private void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
            
            if (Btxlogin.Text == "Login")
            {
                Login dialog = new Login();
                dialog.Owner = this;
                if (dialog.ShowDialog() == true)
                {
                    Btxlogin.Text = "Logout";
                    
                    LvUsers.ItemsSource = GetList();
                    TbkStatus.Text = "welcome: " + App.Current.Properties[1].ToString();
                }
            }
            else if(Btxlogin.Text == "Logout")
            {
                Emp = null;
                App.Current.Properties[0] ="";

                App.Current.Properties[1] = "";
                App.Current.Properties[2] = "";
                App.Current.Properties[3] = "";
                LvUsers.ItemsSource = GetList(); 
                Btxlogin.Text = "Login";
                TbkStatus.Text = "";
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
    {
        ProductsViewDlg dialog = new ProductsViewDlg();
        dialog.Owner = this;
        dialog.ShowDialog();
    }
            
    private void BtnRegister_Click(object sender, RoutedEventArgs e)
    {
            Register dialog = new Register();
            dialog.Owner = this;
            dialog.ShowDialog();
            TbkStatus.Text = "Employee register";
        }



        private void LvUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Employee currEmp = LvUsers.SelectedItem as Employee;
            if (currEmp == null) return;

            EditEmployee dialog = new EditEmployee(currEmp);//==============================================   new
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                LvUsers.ItemsSource = GetList();
                TbkStatus.Text = "Employee updated";
            }
            /*
            else
            {
                //LvUsers.ItemsSource = currEmp;
            }
            */
        }

        private void MenuItem_FileExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_ListEditItemClick(object sender, RoutedEventArgs e)
        {
            LvUsers_MouseDoubleClick(sender, null);
        }

        private void MenuItem_ListDeleteItemClick(object sender, RoutedEventArgs e)
        {
            Employee currEmp = LvUsers.SelectedItem as Employee;
            if (currEmp.name != (string)App.Current.Properties[1]) {
                Globals.dbContext.Employees.Remove(currEmp);
                Globals.dbContext.SaveChanges();
                LvUsers.ItemsSource = GetList();
                TbkStatus.Text = "Employee deleted";
            }
            
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

