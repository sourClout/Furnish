
using MahApps.Metro.Controls.Dialogs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data.Entity;


namespace Furnish
{
    /// <summary>
    /// Interaction logic for EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {


        Employee currEmp;
        public EditEmployee(Employee _currEmp)
        {
            this.currEmp = _currEmp;
            InitializeComponent();

            if (currEmp != null)
            {
                TbxUserName.Text = currEmp.name;
                TbxEmail.Text = currEmp.email;
                ComRole.Text = Enum.GetName(typeof(RoleEnum), currEmp.role);
            }

        }




        private void btSave_Click(object sender, RoutedEventArgs e)
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
                errormessage.Text = "";

                try
                {
                    if ((string)App.Current.Properties[1] == currEmp.name) { 
                    // Globals.dbContext.ChangeTracker.
                    currEmp.name = TbxUserName.Text;
                    currEmp.email = TbxEmail.Text;
                    currEmp.role = (RoleEnum)Enum.Parse(typeof(RoleEnum), ComRole.Text);


                        App.Current.Properties[1] = currEmp.name;
                        App.Current.Properties[2] = currEmp.email;
                        App.Current.Properties[3] = currEmp.role;
                    }
                    else
                    {
                        currEmp.name = TbxUserName.Text;
                        currEmp.email = TbxEmail.Text;
                        currEmp.role = (RoleEnum)Enum.Parse(typeof(RoleEnum), ComRole.Text);
                    }
                    Globals.dbContext.SaveChanges();

                    this.DialogResult = true;
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
        }
    }
}
