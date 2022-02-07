using CalorieDiary.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Day> Days { get; set; }
        public DbSet<EatedProductInDay> EatedProductInDays { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CaloricDemand> CaloricDemands { get; set; }
        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
