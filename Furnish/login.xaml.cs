using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
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

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TbxUserName.Text.Length == 0)
            {
                errormessage.Text = "Enter your User Name.";
                TbxUserName.Focus();
            }
            else if(TbxPassword.Password.Length==0)
            {
                errormessage.Text = "Enter your Password.";
                TbxPassword.Focus();
            }
            else
            {
                string _name = TbxUserName.Text;
                string _password = TbxPassword.Password;

               
                Employee Emp = (from employee in Globals.dbContext.Employees where employee.name == _name && employee.password == _password select employee).FirstOrDefault<Employee>();
                if (Emp != null)
                {
                    App.Current.Properties[0] =Emp.id;

                    App.Current.Properties[1] = Emp.name;
                    App.Current.Properties[2] = Emp.email;
                    App.Current.Properties[3] = Emp.role;
                    this.DialogResult = true;
                    Close();
                }
                else
                {
                    errormessage.Text = ("record  not found");
                }


            }
        }
    }
}

