using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Furniture.Models
{
    public class Customer
    {
        public Customer() { }

        public Customer(int id, string name, string email, string address, string phone) 
        { 
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
        }

        

        private string _id;

        public int Id { get; set; }

        private string _name;

        public string Name { get; set; }

        private string _email;

        public string Email { get; set; }

        private string _address;

        public string Address { get; set; }

        private string _phone;

        public string Phone { get; set; }


    }
    public class Employee
    {

        public Employee() { }
        public Employee(int id, string name, string email, string password, RoleEnum role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }



        private string _id;

        public int Id { get; set; }

        private string _name;

        public string Name { get; set; }

        private string _email;

        public string Email { get; set; }

        private string _password;

        public string Password { get; set; }

        
        public enum RoleEnum
            {
            admin = 0,
            user = 1
            }
        public RoleEnum Role { get; set; }




    }

    public class Product
    {
        public Product() { }

        public Product(int id, string name, string description, string PhotoUrl, decimal price, int qtyAvailable)
        {
            
        }

        public int Id { get; set; }

        public string Name { get; set; }

        //[TextArea]
        public string Description { get; set; }
        //  we need use textarea ,,public Text Description { get; set; }

        public string PhotoUrl { get; set; }

        public decimal Price { get; set; }

        public int QtyAvailable { get; set; }

        
    }
     
    public class Order
    {
        public Order() { }
        public Order(int id, int empId, int cusId, DateTime date, StatusEnum status, string paymentConfirmation)
        {

            Id=id;
            EmpID=empId;
            CustId=cusId;
            Date=date;
            Status=status;
            PaymentConfirmation=paymentConfirmation;
        }

        public int Id { get; set; }

        public int EmpID { get; set; }

        public int CustId { get; set; }

        public DateTime Date { get; set; }


        public enum StatusEnum
        {
            inPreparation = 0,
            paid = 1,
            shipped=2
        }
        public StatusEnum Status { get; set; }

        public string PaymentConfirmation { get; set; }

    }

    public class OrderDetail
    {
        public OrderDetail() { }
        public OrderDetail(int id, int orderId, int productId, int qty, decimal price)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Qty = qty;
            Price = price;
        }

        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        

    }
}
