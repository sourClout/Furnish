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
   
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

      


        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            TbxUserName.Text = "";
            TbxEmail.Text = "";
            TbxPassword.Password = "";
            TbxComfirmPassword.Password = "";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (TbxUserName.Text.Length == 0)
            {
                errormessage.Text = "Enter an name.";
                TbxUserName.Focus();
            }
            else if (TbxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                TbxEmail.Focus();
            }
            else if (!Regex.IsMatch(TbxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                TbxEmail.Select(0, TbxEmail.Text.Length);
                TbxEmail.Focus();
            }
            else
            {
                string name = TbxUserName.Text;
                string email = TbxEmail.Text;
                string password = TbxPassword.Password;
                if (TbxPassword.Password.Length == 0)
                {
                    errormessage.Text = "Enter password.";
                    TbxPassword.Focus();
                }
                else if (TbxComfirmPassword.Password.Length == 0)
                {
                    errormessage.Text = "Enter Confirm password.";
                    TbxComfirmPassword.Focus();
                }
                else if (TbxPassword.Password != TbxComfirmPassword.Password)
                {
                    errormessage.Text = "Confirm password must be same as password.";
                    TbxComfirmPassword.Focus();
                }
                else
                {
                    errormessage.Text = "";
                    
                    SqlConnection con = new SqlConnection("Data Source=fsd04.database.windows.net;Initial Catalog=Furnish;User ID=myadmin;Password=Ouradmin03");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Employees (name,email,password,role) values('" + name + "','"  + email + "','" + password + "', 2 )", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    


                    TbxUserName.Text = "";
                    TbxEmail.Text = "";
                    TbxPassword.Password = "";
                    TbxComfirmPassword.Password = "";
                }
            }
        }
    }
}
