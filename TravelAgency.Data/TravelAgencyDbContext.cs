using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Data.Entities;

namespace TravelAgency.Data
{
   public class TravelAgencyDbContext 
        : DbContext
    {
        public TravelAgencyDbContext(DbContextOptions<TravelAgencyDbContext>options)
            :base(options)
        {
        }

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Destination> Destinations  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Passenger>(passenger =>
            {
                //passenger.Property(p => p.Name).IsRequired();
                passenger.Property(p => p.Id).IsRequired();
                passenger.HasKey(p => p.Id);
                passenger.Property(p => p.Name).HasMaxLength(200).IsRequired();
                passenger.Property(p => p.LastName).HasMaxLength(200).IsRequired();
                passenger.Property(p => p.DoB).IsRequired();
                passenger.Property(p => p.passportNumber).IsRequired();
                passenger.HasMany(p => p.Destinations);
               // passenger.HasOne(p => p.Agents);
            });

            modelBuilder.Entity<Agent>(agent =>
            {
                //passenger.Property(p => p.Name).IsRequired();
                agent.Property(a => a.Id).IsRequired();
                agent.HasKey(a => a.Id);
                agent.Property(a => a.Name).HasMaxLength(200).IsRequired();
                agent.Property(a => a.LastName).HasMaxLength(200).IsRequired();
                agent.Property(a => a.DoB).IsRequired();
                agent.Property(a => a.Title).HasMaxLength(100).IsRequired();
               // agent.HasMany(a => a.Passengers);
                //agent.HasOne(a => a.Destinations);
            });

            modelBuilder.Entity<Airplane>(airplane =>
            {
                airplane.Property(a => a.Id).IsRequired();
                airplane.HasKey(a => a.Id);
                airplane.Property(a => a.FlightNumber).IsRequired();
                airplane.Property(a => a.maxCapacity).IsRequired();
                //airplane.HasOne(a => a.Destinations);
            });

            modelBuilder.Entity<Destination>(destination =>
            {
                destination.Property(d => d.Id).IsRequired();
                destination.HasKey(d => d.Id);
                destination.Property(d => d.City).IsRequired();
                destination.Property(d => d.DepartureDate).IsRequired();

                destination.HasOne(d => d.Passenger)
                 .WithMany(d => d.Destinations)
                  .HasForeignKey(d => d.PassengerId);

                destination.HasOne(d => d.Agent)
               .WithMany(d => d.Destinations)
               .HasForeignKey(d => d.AgentId);
               
            });


        }

    }
}
