using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yogam.AMC.Data.Models;

namespace Yogam.AMC.Web.Infrastructure
{
    public class AppUserManager : UserManager<ApplicationUser>
    {
        public AppUserManager(IUserStore<ApplicationUser> store) : base(store)
        {

        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            ApplicationDbContext db = context.Get<ApplicationDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<ApplicationUser>(db));
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };

            //manager.PasswordValidator = new CustomPasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = false,
            //    RequireLowercase = true,
            //    RequireUppercase = true
            //};

            //manager.UserValidator = new UserValidator<AppUser>(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = true,
            //    RequireUniqueEmail = true
            //};

            //manager.UserValidator = new CustomUserValidator(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = true,
            //    RequireUniqueEmail = true
            //};




            return manager;
        }

    }
}