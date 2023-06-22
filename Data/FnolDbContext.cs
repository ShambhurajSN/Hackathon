using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnolApiVersion2.O.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace FnolApiVersion2.O.Data
{
    //DbContext class that makes our models connected to the Database
    public class FnolDbContext : DbContext
    {
        public FnolDbContext(DbContextOptions<FnolDbContext> options) : base(options)
        {
            
        }
        //Fluent api implementation for the models 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleID);
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.user)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserID);
            modelBuilder.Entity<User>()
                .HasMany<FnolDetails>(x=>x.FnolDetails)
                .WithOne(x => x.userDetails)
                .HasForeignKey(x=>x.UserID);
            modelBuilder.Entity<FnolDetails>()
                .HasOne<IncidentDetails>(x => x.incidentDetails)
                .WithOne(x => x.fnolDetails)
                .HasForeignKey<IncidentDetails>(x => x.FnolID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<IncidentDetails>()
                .HasOne<DriverDetails>(x => x.driverDetails)
                .WithOne(x => x.incidentDetails)
                .HasForeignKey<DriverDetails>(x => x.IncidentID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DriverDetails>()
                .HasOne<VehicleDetails>(x => x.vehicleDetails)
                .WithOne(x => x.driverDetails)
                .HasForeignKey<VehicleDetails>(x => x.DriverID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<IncidentDetails>()
                .HasOne<IncidentPicture>(x => x.incidentPicture)
                .WithOne(x => x.incidentDetails)
                .HasForeignKey<IncidentPicture>(x => x.IncidentID)
                .OnDelete(DeleteBehavior.Cascade);
        }
        //Tables in the Database
        public DbSet<FnolDetails> fnolDetails {get;set;}

        public DbSet<IncidentDetails> incidentDetails {get;set;}

        public DbSet<DriverDetails> driverDetails {get;set;}

        public DbSet<IncidentPicture> incidentPictures {get;set;}

        public DbSet<VehicleDetails> vehicleDetails {get; set;}

        public DbSet<User_Role> User_Roles {get;set;}
        public DbSet<Role> Roles {get;set;}
        public DbSet<User> Users {get;set;}
    }
}