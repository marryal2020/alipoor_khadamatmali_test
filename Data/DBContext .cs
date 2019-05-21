using Microsoft.EntityFrameworkCore;
using alipoor_test.Models;


namespace alipoor_test.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Person> Persons { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Person>().ToTable("Person");

        //    modelBuilder.Entity<Address>().ToTable("Address");
        ////    modelBuilder.Entity<Address>()
        ////        .HasKey(c => new { c.AddressID, c.NationalID });
        ////

        //}


    }
}