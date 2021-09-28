using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data;
using TravelAgency.Data.Entities;
using TravelAgency.Models.Models.Airplane;
using TravelAgency.Services.Abstraction;

namespace TravelAgency.Services.Services
{
    public class AirplaneService 
        : IAirplaneService
    {
        private readonly TravelAgencyDbContext _context;
        private readonly IMapper _mapper;

        public AirplaneService(TravelAgencyDbContext context, IMapper mapper)
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

        public async Task<IEnumerable<AirplaneModelBase>> Get()
        {
            var agents = await _context.Airplanes.ToListAsync();
            return _mapper.Map<IEnumerable<AirplaneModelBase>>(agents);
        }

        public async Task<AirplaneModelExtended> Get(int id)
        {
            var airplane = await _context.Airplanes
                            .Include(d => d.Destinations)
                            .ThenInclude(p => p.Passenger)
                            .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<AirplaneModelExtended>(airplane);
        }

        public async Task<AirplaneModelBase> Insert(AirplaneModelCreate model)
        {
            var entity = _mapper.Map<Airplane>(model);
            await _context.Airplanes.AddAsync(entity);
            await SaveAsync();

            return _mapper.Map<AirplaneModelBase>(entity);
        }

        public async Task<AirplaneModelBase> Update(AirplaneModelUpdate model)
        {
            var entity = _mapper.Map<Airplane>(model);
            _context.Airplanes.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await SaveAsync();
            return _mapper.Map<AirplaneModelBase>(entity);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
