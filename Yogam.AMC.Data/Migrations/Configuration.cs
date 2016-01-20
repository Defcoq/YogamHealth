namespace Yogam.AMC.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Yogam.AMC.Data.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Yogam.AMC.Data.Models.ApplicationDbContext context)
        {
            ApplicationUserManager userMgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            ApplicationRoleManager roleMgr = new ApplicationRoleManager(new RoleStore<AppRole>(context));
            string roleName = "Administrators";
            string userName = "YogamAdmin";
            string password = "Yogam@1";
            string email = "admin@yogamhealth.com";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }
            ApplicationUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new ApplicationUser { UserName = userName, Email = email },
                password);
                user = userMgr.FindByName(userName);
            }
            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }
            context.SaveChanges();

        }
    }
}
