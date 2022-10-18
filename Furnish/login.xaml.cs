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
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TbxUserName.Text.Length == 0)
            {
                errormessage.Text = "Enter your User Name.";
                TbxUserName.Focus();
            }
            else
            {
                string name = TbxUserName.Text;
                string password = TbxPassword.Password;
                SqlConnection con = new SqlConnection("Data Source=fsd04.database.windows.net;Initial Catalog=Furnish;User ID=myadmin;Password=Ouradmin03");
                con.Open();
                SqlCommand cmd = null;

                cmd = new SqlCommand("Select * from Employees where Name ='" + name + "'  and password='" + password + "'", con);
                SqlDataReader rdr = cmd.ExecuteReader();
      

                //while (rdr.Read())
                //{
                //    // get the results of each column
                //    name = (string)rdr["name"];
                //    string email = (string)rdr["email"];
                //    string role = (string)rdr["role"];


                //}


                if (rdr != null)
                {

                    Close();
                    con.Close();
                }
                else
                {

                    errormessage.Text = "Sorry! Please enter existing emailid/password.";

                }

            }
        }

        //  get data from database, display in the home page
       


    }

       

    }

