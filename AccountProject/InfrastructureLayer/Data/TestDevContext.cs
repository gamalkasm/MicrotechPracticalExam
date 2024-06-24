using AccountProject.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AccountProject.InfrastructureLayer.Data
{
    public class TestDevContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public TestDevContext(DbContextOptions<TestDevContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().HasKey(a => a.ACC_Number);
        }
    }
}
