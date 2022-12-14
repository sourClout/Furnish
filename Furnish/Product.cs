//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Furnish
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int qtyAvailable { get; set; }
        public byte[] image { get; set; }

        public Product(string name, string description, decimal price, int qtyAvailable, byte[] image)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.qtyAvailable = qtyAvailable;
            this.image = image;
        }

        public Product(string name, string description, decimal price, int qtyAvailable)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.qtyAvailable = qtyAvailable;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
