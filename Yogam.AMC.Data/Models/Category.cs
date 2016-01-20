namespace Yogam.AMC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
