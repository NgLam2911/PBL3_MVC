using System.Data.Entity;
using PBL3_MVC.Data.Tables;

namespace PBL3_MVC.Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<Db>
    {
        protected override void Seed(Db context)
        {
            context.Roles.AddRange(new Role[]
            {
                new Role() {RoleID = 1, RoleName = "Admin"},
                new Role() {RoleID = 2, RoleName = "BusStation"},
            });
            //TODO: Add default admin account
        }
    }
}