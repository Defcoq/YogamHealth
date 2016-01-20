namespace Yogam.AMC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initRoleCavollo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "IdentityNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "YogamTemporyCardNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "Nationality", c => c.String());
            AddColumn("dbo.AspNetUsers", "Living", c => c.String());
            AddColumn("dbo.AspNetUsers", "Currency", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "Fistname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Lastname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Lastname");
            DropColumn("dbo.AspNetUsers", "Fistname");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Currency");
            DropColumn("dbo.AspNetUsers", "Living");
            DropColumn("dbo.AspNetUsers", "Nationality");
            DropColumn("dbo.AspNetUsers", "YogamTemporyCardNumber");
            DropColumn("dbo.AspNetUsers", "IdentityNumber");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Country");
        }
    }
}
