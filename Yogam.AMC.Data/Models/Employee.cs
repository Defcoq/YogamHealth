using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yogam.AMC.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            ReportingEmployees = new HashSet<Employee>();
            Orders = new HashSet<Order>();
            Territories = new HashSet<Territory>();
        }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(25)]
        [Display(Name = "Title Of Courtesy")]
        public string TitleOfCourtesy { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Country { get; set; }

        [StringLength(24)]
        public string HomePhone { get; set; }

        [StringLength(4)]
        public string Extension { get; set; }

        public string Notes { get; set; }
        public string Email { get; set; }

        public int? ReportsTo { get; set; }

        public virtual ICollection<Employee> ReportingEmployees { get; set; }
        public virtual Employee ReportsToEmployee { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
