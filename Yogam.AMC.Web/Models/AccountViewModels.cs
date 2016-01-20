using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yogam.AMC.Data.Models;

namespace Yogam.AMC.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class UserRegistrationData
    {

        public string Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

        public string Country { get; set; }
       
        public string State { get; set; }

        public string Gender { get; set; }

        public string Fistname { get; set; }

        public string Lastname { get; set; }

        public string Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
       
        public string City { get; set; }

        public string Nationality { get; set; }

        public string Living { get; set; }
        public string IdentityNumber { get; set; }

        public string YogamCardNumber { get; set; }
        public Status Status { get; set; }
        public StatusCode StatusCode { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }

    }


    public class RegisterViewModel2
    {
       
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Fistname")]
        public string Fistname { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

       // [Required]
        [Display(Name = "Y. Girl Name (optional)")]
        public string Surname { get; set; }

       // [Required]
       // [DataType(DataType.Date)]
        [Display(Name = "Date Of birth (optional)")]
       // [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
       // public string DateOfBirth { get; set; }

      //  [Required]
        [Display(Name = "Phone Number (optional)")]
        public string PhoneNumber { get; set; }

        
    }
    public class RegisterViewModel3
    {
     
        [Required]
        [Display(Name = "What is your bith city ?")]
        public string City { get; set; }


        [Required]
        [Display(Name = "What is Your nationality ?")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "In wich city do you live ?")]
        public string Living { get; set; }

        [Required]
        [Display(Name = "Document Type")]
        public DocumentType DocumentType { get; set; }
    

      // [Required]
        [Display(Name = "Passport or Identity card number (optional)")]
        public string IdentityNumber { get; set; }
    }



    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }

}
