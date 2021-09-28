using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Agent;

namespace TravelAgency.Services.Abstraction
{
    public interface IAgentService
    {
        Task<IEnumerable<AgentModelBase>> Get();
        Task<AgentModelExtended> Get(int id);
        Task<AgentModelBase> Insert(AgentCreateModel model);
        Task<AgentModelBase> Update(AgentUpdateModel model);
        Task<bool> Delete(int id);
    }
}
