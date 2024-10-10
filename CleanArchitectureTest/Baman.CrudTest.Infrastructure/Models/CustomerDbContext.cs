using Baman.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Baman.CrudTest.Infrastructure.Models;

public class CustomerDbContext : DbContext
{
    private IConfiguration _config;

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options, IConfiguration config) :
        base(options)
    {
        _config = config;
        Database.SetCommandTimeout(9000);
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config["ConnectionStrings:DefaultConnection"], o =>
        {
            o.CommandTimeout(9000);
        });
      
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customers");
       // using attributes is cleaner compared to this. but needs to redefine model for sql. this way doesnt need that
        modelBuilder.Entity<Customer>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired(); 
        modelBuilder.Entity<Customer>().Property(e => e.FirstName).HasColumnType("nvarchar(20)");
        modelBuilder.Entity<Customer>().Property(e => e.LastName).HasColumnType("nvarchar(20)");
        modelBuilder.Entity<Customer>().Property(e => e.BankAccountNumber).HasColumnType("nvarchar(20)");
        modelBuilder.Entity<Customer>().Property(e => e.PhoneNumber).HasColumnType("nvarchar(20)");
        modelBuilder.Entity<Customer>().Property(e => e.Email).HasColumnType("nvarchar(20)");
        modelBuilder.Entity<Customer>().HasIndex(e => e.Email).IsUnique();


        modelBuilder.Entity<CustomerEvent>().ToTable("CustomerEvents");
        modelBuilder.Entity<CustomerEvent>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired(); 
        modelBuilder.Entity<CustomerEvent>().Property(e => e.FirstName).HasColumnType("nvarchar(20)");
        modelBuilder.Entity<CustomerEvent>().Property(e => e.LastName).HasColumnType("nvarchar(20)");
        modelBuilder.Entity<CustomerEvent>().Property(e => e.BankAccountNumber).HasColumnType("nvarchar(20)");
        modelBuilder.Entity<CustomerEvent>().Property(e => e.PhoneNumber).HasColumnType("nvarchar(20)");
        modelBuilder.Entity<CustomerEvent>().Property(e => e.Email).HasColumnType("nvarchar(20)");
      
        
    }


    public DbSet<Customer> Customers { get; set; }
    // better used event specialized database
    public DbSet<CustomerEvent> CustomerEvents { get; set; }
}