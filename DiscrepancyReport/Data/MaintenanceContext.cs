using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscrepancyReport.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscrepancyReport.Data
{
    public class MaintenanceContext : DbContext
    {
        public MaintenanceContext(DbContextOptions<MaintenanceContext> options) : base(options)
        {

        }

        // creating the entity sets for the database
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<AircraftLocationAssignment> AircraftLocationAssignments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AircraftModel> AircraftModels { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>().ToTable("Aircraft");
            modelBuilder.Entity<AircraftLocationAssignment>().ToTable("Aircraft_Location_Assignment");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<AircraftModel>().ToTable("Aircraft_Model");
            modelBuilder.Entity<Title>().ToTable("Title");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            
        }
    }
}
