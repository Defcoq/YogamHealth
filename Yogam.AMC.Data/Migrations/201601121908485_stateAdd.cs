namespace Yogam.AMC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stateAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "State", c => c.String());
            AddColumn("dbo.AspNetUsers", "ParentUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ParentUserId");
            DropColumn("dbo.AspNetUsers", "State");
        }
    }
}
