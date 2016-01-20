using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yogam.AMC.Data.Models
{
    public class Territory
    {
        public Territory()
        {
            Employees = new HashSet<Employee>();
        }

        [Required]
        [StringLength(50)]
        public string TerritoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }

        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
