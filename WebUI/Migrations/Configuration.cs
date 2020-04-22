using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebUI.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WebUI.Models.ApplicationContext";
        }

        protected override void Seed(WebUI.Models.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
