namespace Yogam.AMC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CardNum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "YogamCardNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "StatusCode", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ValidFrom", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "ValidTo", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "EnrollmentDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "ModifiedDate");
            DropColumn("dbo.AspNetUsers", "EnrollmentDate");
            DropColumn("dbo.AspNetUsers", "ValidTo");
            DropColumn("dbo.AspNetUsers", "ValidFrom");
            DropColumn("dbo.AspNetUsers", "StatusCode");
            DropColumn("dbo.AspNetUsers", "Status");
            DropColumn("dbo.AspNetUsers", "YogamCardNumber");
        }
    }
}
