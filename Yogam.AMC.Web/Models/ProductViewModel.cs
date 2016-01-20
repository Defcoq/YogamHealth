namespace Yogam.AMC.Web.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
        public string Status { get; set; }
    }
}