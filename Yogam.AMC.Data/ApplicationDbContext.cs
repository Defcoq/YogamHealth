using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Yogam.AMC.Data.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //DefaultConnection DefaultConnection
        public ApplicationDbContext()
           : base("YogamAMC")
           //: base("DefaultConnection")
        {
          // System.Data.Entity.Database.SetInitializer<ApplicationDbContext>(null);
            Database.SetInitializer<ApplicationDbContext>(new IdentityDbInit());

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().HasMany(e => e.ReportingEmployees)
                .WithOptional(e => e.ReportsToEmployee)
                .HasForeignKey(e => e.ReportsTo);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Territories)
                .WithMany(e => e.Employees)
                .Map(m => m.ToTable("EmployeeTerritories").MapLeftKey("EmployeeID").MapRightKey("TerritoryID"));

            modelBuilder.Entity<OrderDetail>().Property(e => e.UnitPrice).HasPrecision(19, 4);

            modelBuilder.Entity<Order>().Property(e => e.CustomerCode).IsFixedLength();

            modelBuilder.Entity<Order>().Property(e => e.Freight).HasPrecision(19, 4);

            modelBuilder.Entity<Order>().HasMany(e => e.OrderDetails).WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Product>().HasMany(e => e.OrderDetails).WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>().Property(e => e.RegionDescription).IsFixedLength();

            modelBuilder.Entity<Region>().HasMany(e => e.Territories).WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipper>().HasMany(e => e.Orders).WithOptional(e => e.Shipper)
                .HasForeignKey(e => e.ShipVia);

            modelBuilder.Entity<Territory>().Property(e => e.TerritoryDescription).IsFixedLength();

            base.OnModelCreating(modelBuilder);
        }
    }

    public class IdentityDbInit: NullDatabaseInitializer<ApplicationDbContext> //DropCreateDatabaseAlways<ApplicationDbContext>
 //
    {
        //protected override void Seed(ApplicationDbContext context)
        //{
        //    PerformInitialSetup(context);
        //    base.Seed(context);
        //}
        public void PerformInitialSetup(ApplicationDbContext context)
        {
           // ApplicationUserManager userMgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            //    ApplicationRoleManager roleMgr = new ApplicationRoleManager(new RoleStore<AppRole>(context));
            //    string roleName = "Administrators";
            //    string userName = "YogamAdmin";
            //    string password = "Yogam@1";
            //    string email = "admin@yogamhealth.com";
            //    if (!roleMgr.RoleExists(roleName))
            //    {
            //        roleMgr.Create(new AppRole(roleName));
            //    }
            //    ApplicationUser user = userMgr.FindByName(userName);
            //    if (user == null)
            //    {
            //        userMgr.Create(new ApplicationUser { UserName = userName, Email = email },
            //        password);
            //        user = userMgr.FindByName(userName);
            //    }
            //    if (!userMgr.IsInRole(user.Id, roleName))
            //    {
            //        userMgr.AddToRole(user.Id, roleName);
            //    }
            //    context.SaveChanges();

        }
    }

}