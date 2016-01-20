using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Yogam.AMC.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string IdentityNumber { get; set; }
        public string YogamTemporyCardNumber { get; set; }
        public string Nationality { get; set; }
        public string Living { get; set; }
        public string Currency { get; set; }
        public string Gender { get; set; }
        public string Fistname { get; set; }
        public string Lastname { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ParentUserId { get; set; }
        public DocumentType DocumentType { get; set; }
        public string YogamCardNumber { get; set; }
        public Status Status { get; set; }
        public StatusCode StatusCode { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public DateTime? ModifiedDate { get; set; }


    }

    public enum DocumentType
    {
        [Display(Name = "Identity Card")]
        IdentityCard,
        [Display(Name = "Passport")]
        Passport,
        [Display(Name = "Driver Licence")]
        DriverLicence
    }

    public enum Gender
    {
        [Display(Name = "M")]
        M,
        [Display(Name = "F")]
        F
       
    }

    public enum Status
    {
        [Display(Name = "Enable")]
        Enable,
        [Display(Name = "Disable")]
        Disable

    }

    public enum StatusCode
    {
        [Display(Name = "Cancel")]
        Cancel,
        [Display(Name = "Dismissed")]
        Dismissed,


        [Display(Name = "Paid")]
        Paid,
        [Display(Name = "Registre only")]
        RegistreOnly

    }


}