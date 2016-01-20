using System.ComponentModel;

namespace Yogam.AMC.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(5)]
        [DisplayName("Customer Code")]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(40)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [StringLength(30)]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        [StringLength(30)]
        [DisplayName("Contact Title")]
        public string ContactTitle { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

         [DisplayName("Region")]
        public int? RegionId { get; set; }

        [StringLength(50)]
        [DisplayName("Territory")]
        public string TerritoryId { get; set; }

        [StringLength(10)]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Country { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        public int CustomerType { get; set; }

        public virtual Region Region { get; set; }

        public virtual Territory Territory { get; set; }
    }
}
