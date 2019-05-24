namespace OnlineCv.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailModels", "IsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailModels", "IsRead");
        }
    }
}
