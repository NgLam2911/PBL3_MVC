using System.Data.Entity;
using PBL3_MVC.Data.Tables;

namespace PBL3_MVC.Data
{
    //Sealed class cannot be inherited, just like "final" in PHP ...
    public sealed class Db: DbContext
    {
        public Db(): base("name=PBL3_MVC")
        {
            Database.SetInitializer(new DbInitializer());
        }

        #region DbSets
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusStation> BusStations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Location> Locations { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //For relationship and properties in tables
            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .HasOptional(e => e.BusStation)
                .WithRequired(e => e.Account);

            modelBuilder.Entity<Account>()
                .HasOptional(e => e.Customer)
                .WithRequired(e => e.Account);

            modelBuilder.Entity<Bus>()
                .HasMany(e => e.Schedules)
                .WithRequired(e => e.Bus)
                .WillCascadeOnDelete(false); //Wtf ?

            modelBuilder.Entity<BusStation>()
                .HasMany(e => e.Buses)
                .WithRequired(e => e.BusStation)
                .HasForeignKey(e => e.BusStationID) //EYYO ????
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CustomerID) //EYYO ????
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Schedules)
                .WithRequired(e => e.Route)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Schedule>()
                .HasMany(e => e.Seats)
                .WithRequired(e => e.Schedule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.DeparturesRoute)
                .WithRequired(e => e.Departure)
                .HasForeignKey(e => e.DepartureID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.DestinationsRoute)
                .WithRequired(e => e.Destination)
                .HasForeignKey (e => e.DestinationID)
                .WillCascadeOnDelete(false);
        }
    }
}
