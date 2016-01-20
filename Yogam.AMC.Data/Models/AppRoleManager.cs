using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yogam.AMC.Data.Models;

namespace Yogam.AMC.Data.Models
{
    public class ApplicationRoleManager : RoleManager<AppRole>, IDisposable
    {
        public ApplicationRoleManager(RoleStore<AppRole> store) : base(store) { }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new
            RoleStore<AppRole>(context.Get<ApplicationDbContext>()));
        }

    }
}