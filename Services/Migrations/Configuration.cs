using System.Data.Entity.Migrations;
using Services.Data.Context;

namespace Services.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            // Initial Installer Code
        }
    }
}
