using AppTaxi2020.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Data
{
    public class AppDataContext : IdentityDbContext<UserEntity>
    {
        public AppDataContext(DbContextOptions<AppDataContext> options):base(options)
        {

        }

        public DbSet<TaxiEntity> Taxis { get; set; }
        public DbSet<TripDetailEntity> TripDetails { get; set; }
        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<UserGroupEntity> UserGroupEntities { get; set; }
        public DbSet<UserGroupDetailEntity> UserGroupDetailEntities { get; set; }
        public DbSet<UserGroupRequestEntity> UserGroupRequestEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxiEntity>().HasIndex(t => t.Plaque).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
