

using System.Data.Entity;

using Furniture.Models;

namespace Furniture.Database
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(string ConnectionString): base(ConnectionString)
        {
            //throw new System.NotImplementedException();
        }
        public IDbSet<Customer> Customers{ get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Order> EOrders { get; set; }
        public IDbSet<OrderDetail> OrderDetails { get; set; }
    }
}
