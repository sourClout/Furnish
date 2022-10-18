﻿using System;
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

namespace Furnish
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomerDialog custDialog = new CustomerDialog();
            custDialog.Owner = this;
            custDialog.ShowDialog();
        }
    }
}
