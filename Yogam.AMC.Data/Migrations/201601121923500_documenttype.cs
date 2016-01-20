namespace Yogam.AMC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documenttype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DocumentType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DocumentType");
        }
    }
}
