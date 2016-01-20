using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yogam.AMC.Data.Models
{
    [Table("Region")]
    public partial class Region
    {
        public Region()
        {
            Customers = new HashSet<Customer>();
            Territories = new HashSet<Territory>();
        }

        public int RegionId { get; set; }

        [Required]
        [StringLength(50)]
        public string RegionDescription { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Territory> Territories { get; set; }
    }
}
