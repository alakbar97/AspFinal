namespace OnlineCv.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicBackgrounds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qualification = c.String(nullable: false),
                        Degree = c.String(nullable: false),
                        UniversityName = c.String(),
                        YearofObtention = c.String(),
                        Details = c.String(),
                        mediaUrl = c.String(),
                        CreationDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 70),
                        Password = c.String(nullable: false, maxLength: 20),
                        CreationDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        Description = c.String(),
                        SkillLevel = c.String(),
                        SkillDescription = c.String(),
                        IsBar = c.Boolean(nullable: false),
                        IsTag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Age = c.Int(nullable: false),
                        Location = c.String(nullable: false, maxLength: 70),
                        Experience = c.String(),
                        Degree = c.String(nullable: false, maxLength: 70),
                        MediaUrl = c.String(),
                        CareerLevel = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        CreationDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfessionalExperiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duration = c.String(nullable: false),
                        JobTitle = c.String(nullable: false, maxLength: 50),
                        CompanyName = c.String(nullable: false, maxLength: 50),
                        MediaUrl = c.String(),
                        Location = c.String(),
                        Details = c.String(),
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
            DropForeignKey("dbo.Bios", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.Bios", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Bios", new[] { "SkillId" });
            DropIndex("dbo.Bios", new[] { "CategoryId" });
            DropTable("dbo.ProfessionalExperiences");
            DropTable("dbo.PersonDetails");
            DropTable("dbo.Skills");
            DropTable("dbo.Categories");
            DropTable("dbo.Bios");
            DropTable("dbo.Admins");
            DropTable("dbo.AcademicBackgrounds");
        }
    }
}
