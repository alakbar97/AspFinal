namespace OnlineCv.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AcademicBackgrounds", "Qualification", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.AcademicBackgrounds", "Degree", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.AcademicBackgrounds", "UniversityName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Skills", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skills", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.AcademicBackgrounds", "UniversityName", c => c.String());
            AlterColumn("dbo.AcademicBackgrounds", "Degree", c => c.String(nullable: false));
            AlterColumn("dbo.AcademicBackgrounds", "Qualification", c => c.String(nullable: false));
        }
    }
}
