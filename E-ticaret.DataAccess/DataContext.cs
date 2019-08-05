using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using E_ticaret.DataAccess.Identity;

namespace E_ticaret.DataAccess
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {        
        public DataContext()
        {          
            
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"server=(localdb)\MSSQLLocalDB;database=DbTicaret;Trusted_Connection=True;ConnectRetryCount=0";
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        
    }
}
