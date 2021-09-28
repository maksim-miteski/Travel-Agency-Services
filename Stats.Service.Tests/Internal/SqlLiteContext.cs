using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data;
using TravelAgency.Data.Entities;

namespace Stats.Service.Tests.Internal
{
    public abstract class SqlLiteContext 
        :IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;
        protected readonly TravelAgencyDbContext DbContext;


        protected SqlLiteContext(bool withData = false)
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            DbContext = new TravelAgencyDbContext(CreateOptions());
            _connection.Open();
            DbContext.Database.EnsureCreated();
            //SeedData(DbContext);

              if (withData)
               SeedData(DbContext);
        }

        private DbContextOptions<TravelAgencyDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<TravelAgencyDbContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseSqlite(_connection)
                .Options;
        }


        private void SeedData(TravelAgencyDbContext dbContext)
        {
            var agents = new List<Agent>
            {
              new Agent
              {
                  Id=1,
                  Name="Agent's name1",
                  LastName="Agent's Last Name1",
                  DoB=DateTime.Today.AddYears(-1),
                  Title="Title1"
              }
            };
            var passengers = new List<Passenger>
            {
                new Passenger
                {
                    Id=1,
                    Name="Passenger's Name 1",
                    LastName="Passengers Last Name 1",
                    DoB=DateTime.Today.AddYears(-18),
                    passportNumber=1234567
                },
                new Passenger
                {
                    Id=2,
                    Name="Passenger's Name 2",
                    LastName="Passengers Last Name 2",
                    DoB=DateTime.Today.AddYears(-27),
                    passportNumber=1234567
                },
                new Passenger
                {
                    Id=3,
                    Name="Passenger's Name 3",
                    LastName="Passengers Last Name 3",
                    DoB=DateTime.Today.AddYears(-19),
                    passportNumber=1234567
                }
            };
            var destinations = new List<Destination>
            {
                new Destination
                {
                    Id=1,
                    City="Rome",
                    DepartureDate=DateTime.Today.AddDays(10),
                    PassengerId=1,
                    AgentId=1
                },
                new Destination
                {
                    Id=2,
                    City="Rome",
                    DepartureDate=DateTime.Today.AddDays(10),
                    PassengerId=2,
                    AgentId=1
                },
                new Destination
                {
                    Id=3,
                    City="Paris",
                    DepartureDate=DateTime.Today.AddDays(50),
                    PassengerId=3,
                    AgentId=1
                },
                new Destination
                {
                    Id=4,
                    City="Paris",
                    DepartureDate=DateTime.Today.AddDays(50),
                    PassengerId=4,
                    AgentId=1
                }
            };

            dbContext.AddRange(agents);
            dbContext.AddRange(passengers);
            dbContext.AddRange(destinations);
            dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _connection.Close();
            _connection?.Dispose();
            DbContext?.Dispose();
        }
    }
}
