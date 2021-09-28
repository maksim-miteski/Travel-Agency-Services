using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data;
using TravelAgency.Data.Entities;
using TravelAgency.Models.Models.Agent;
using TravelAgency.Services.Abstraction;

namespace TravelAgency.Services.Services
{
    public class AgentService
        : IAgentService
    {
        private readonly TravelAgencyDbContext _context;
        private readonly IMapper _mapper;

        public AgentService(TravelAgencyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            var agent = await _context.Agents.FindAsync(id);
            _context.Agents.Remove(agent);
            return await SaveAsync() > 0;
        }

        public async Task<IEnumerable<AgentModelBase>> Get()
        {
            var agents = await _context.Agents.ToListAsync();
            return _mapper.Map<IEnumerable<AgentModelBase>>(agents);
        }

        public async Task<AgentModelExtended> Get(int id)
        {
            var agent = await _context.Agents
                .Include(d => d.Destinations)
                .ThenInclude(p => p.Passenger)
                .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<AgentModelExtended>(agent);
        }

        public async Task<AgentModelBase> Insert(AgentCreateModel model)
        {
            var entity = _mapper.Map<Agent>(model);
            await _context.Agents.AddAsync(entity);
            await SaveAsync();

            return _mapper.Map<AgentModelBase>(entity);
        }

        public async Task<AgentModelBase> Update(AgentUpdateModel model)
        {
            var entity = _mapper.Map<Agent>(model);
            _context.Agents.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await SaveAsync();
            return _mapper.Map<AgentModelBase>(entity);
        }

        public async Task<int>SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
