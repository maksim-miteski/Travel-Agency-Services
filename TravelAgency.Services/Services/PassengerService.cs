using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data;
using TravelAgency.Data.Entities;
using TravelAgency.Models.Models.Passenger;
using TravelAgency.Services.Abstraction;

namespace TravelAgency.Services.Services
{
    public class PassengerService
        : IPassengerService
    {
        private readonly TravelAgencyDbContext _context;
        private readonly IMapper _mapper;

        public PassengerService(TravelAgencyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Passengers.FindAsync(id);
            _context.Passengers.Remove(entity);
            return await SaveAsync() > 0;
        }

        public async Task<IEnumerable<PassengerModelBase>> Get()
        {
            var passengers = await _context.Passengers.ToListAsync();
            return _mapper.Map<IEnumerable<PassengerModelBase>>(passengers);
        }

        public async Task<PassengerModelExtended> Get(int id)
        {
            var passenger = await _context.Passengers
                     .Include(d => d.Destinations)
                     .ThenInclude(a => a.Airplane)
                     .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<PassengerModelExtended>(passenger);
        }

        public async Task<PassengerModelBase> Insert(PassengerModelCreate model)
        {
            var entity = _mapper.Map<Passenger>(model);
            await _context.Passengers.AddAsync(entity);
            await SaveAsync();

            return _mapper.Map<PassengerModelBase>(entity);
        }

        public async Task<PassengerModelBase> Update(PassengerModelUpdate model)
        {
            var entity = _mapper.Map<Passenger>(model);
            _context.Passengers.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await SaveAsync();
            return _mapper.Map<PassengerModelBase>(entity);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
