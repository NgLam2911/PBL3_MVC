using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using PBL3_MVC.Data.Tables;

namespace PBL3_MVC.Data
{
    public class Db: DbContext
    {
        public Db(): base("name=PBL3_MVC")
        {
            Database.SetInitializer(new DbInitializer());
        }

        #region DbSets
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<BusStation> BusStations { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //For relationship between tables
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
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BusStation>()
                .HasMany(e => e.Buses)
                .WithRequired(e => e.BusStation)
                .HasForeignKey(e => e.BusStationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CustomerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Schedules)
                .WithRequired(e => e.Route)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Schedule>()
                .HasMany(e => e.Seats)
                .WithRequired(e => e.Schedule)
                .WillCascadeOnDelete(false);
        }
    }
}
