namespace Yogam.AMC.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public float Discount { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
