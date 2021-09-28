using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data;
using TravelAgency.Data.Entities;
using TravelAgency.Models.Models.Destination;
using TravelAgency.Services.Abstraction;

namespace TravelAgency.Services.Services
{
    public class DestinationService
        : IDestinationService
    {
        private readonly TravelAgencyDbContext _context;
        private readonly IMapper _mapper;

        public DestinationService(TravelAgencyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Airplanes.FindAsync(id);
            _context.Airplanes.Remove(entity);
            return await SaveAsync() > 0;
        }

        public async Task<IEnumerable<DestinationModelBase>> GetAgentById(int id)
        {
            var agents = await _context.Destinations
                .Include(d => d.Agent)
                .Include(d => d.Airplane)
                .Include(d => d.Passenger)
                .Where(d => d.AgentId == id).ToListAsync();

            return (IEnumerable<DestinationModelBase>)_mapper.Map<IEnumerable<Destination>>(agents);
        }

        public async Task<IEnumerable<DestinationModelBase>> GetAirplaneById(int id)
        {
            var airplane = await _context.Destinations
                           .Include(d => d.Agent)
                           .Include(d => d.Airplane)
                           .Include(d => d.Passenger)
                           .Where(d => d.AirplaneId == id).ToListAsync();

            return (IEnumerable<DestinationModelBase>)_mapper.Map<IEnumerable<Destination>>(airplane);
        }

        public async Task<IEnumerable<DestinationModelBase>> GetPassengerById(int id)
        {
            var passenger = await _context.Destinations
                          .Include(d => d.Agent)
                          .Include(d => d.Airplane)
                          .Include(d => d.Passenger)
                          .Where(d => d.PassengerId == id).ToListAsync();

            return (IEnumerable<DestinationModelBase>)_mapper.Map<IEnumerable<Destination>>(passenger);
        }

        public async Task<DestinationModelBase> Insert(DestinationModelCreate model)
        {
            var entity = _mapper.Map<Destination>(model);
            await _context.Destinations.AddAsync(entity);
            await SaveAsync();

            return _mapper.Map<DestinationModelBase>(entity);
        }

        public async Task<DestinationModelBase> Update(DestinationModelUpdate model)
        {
            var entity = _mapper.Map<Destination>(model);
            _context.Destinations.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await SaveAsync();
            return _mapper.Map<DestinationModelBase>(entity);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

       
    }
}
