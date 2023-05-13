using PBL3_MVC.Data.Tables;

namespace PBL3_MVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PBL3_MVC.Data.Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PBL3_MVC.Data.Db context)
        {
            context.Roles.AddRange(new Role[]
            {
                new Role() {RoleID = 1, RoleName = "Admin"},
                new Role() {RoleID = 2, RoleName = "BusStation"},
            });
        }
    }
}
