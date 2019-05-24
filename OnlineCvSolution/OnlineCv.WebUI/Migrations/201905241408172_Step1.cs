namespace OnlineCv.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ToMails = c.String(),
                        UserEmail = c.String(nullable: false, maxLength: 50),
                        Subject = c.String(),
                        Body = c.String(nullable: false, maxLength: 500),
                        CreationDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailModels");
        }
    }
}
