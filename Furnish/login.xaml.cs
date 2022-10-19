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
            else if(TbxPassword.Password.Length==0)
            {
                errormessage.Text = "Enter your Password.";
                TbxPassword.Focus();
            }
            else
            {
                string _name = TbxUserName.Text;
                string _password = TbxPassword.Password;

                FurnishDbConnection fdb = new FurnishDbConnection();
                Employee Emp = (from employee in fdb.Employees where employee.name == _name && employee.password == _password select employee).FirstOrDefault<Employee>();
                if (Emp != null)
                {

                    //if(Emp.role == (RoleEnum)2) { 
                    //}
                    this.DialogResult = true;
                    Close();
                }
                else
                {
                    errormessage.Text = ("record  not found");
                }


                //SqlConnection con = new SqlConnection("Data Source=fsd04.database.windows.net;Initial Catalog=Furnish;User ID=myadmin;Password=Ouradmin03");
                //con.Open();
                //SqlCommand cmd = null;

                //cmd = new SqlCommand("Select * from Employees where Name ='" + name + "'  and password='" + password + "'", con);
                //SqlDataReader rdr = cmd.ExecuteReader();




                //if (rdr != null)
                //{

                //    Close();
                //    con.Close();
                //}
                //else
                //{

                //    errormessage.Text = "Sorry! Please enter existing emailid/password.";

                //}

            }
        }
    }
}

