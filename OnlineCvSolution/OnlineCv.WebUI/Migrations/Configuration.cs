namespace OnlineCv.WebUI.Migrations
{
    using OnlineCv.WebUI.Models.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineCv.WebUI.Models.OnlineCvDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OnlineCv.WebUI.Models.OnlineCvDbContext context)
        {
            try
            {

                if (!context.Category.Any())
                {
                    context.Category.AddRange(new[] {
                    new Category{
                        Name="FrontEnd",
                        CreationDate=DateTime.UtcNow
                    },
                    new Category{
                        Name="BackEnd",
                        CreationDate=DateTime.UtcNow
                    }
                         });
                }
                if (!context.Skill.Any())
                {
                    context.Category.AddRange(new[] {
                    new Category{
                        Name="Html5",
                        CreationDate=DateTime.UtcNow
                    },
                    new Category{
                        Name="Css",
                        CreationDate=DateTime.UtcNow
                    }
                         });
                }

                context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;


                File.AppendAllText(@"F:\error.log", ex.ToString());
            }
        }
    }
}
