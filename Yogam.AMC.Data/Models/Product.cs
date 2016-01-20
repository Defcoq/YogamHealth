namespace Yogam.AMC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }
  
        [Display(Name = "Unit Price")]
        public int? UnitPrice { get; set; }

        [Display(Name = "Units In Stock")]
        public int? UnitsInStock { get; set; }

        [Display(Name = "Units On Order")]
        public int? UnitsOnOrder { get; set; }

        [Display(Name = "Reorder Level")]
        public int? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
