using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Destination;

namespace TravelAgency.Services.Abstraction
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationModelBase>> GetAgentById(int id);
        Task<IEnumerable<DestinationModelBase>> GetAirplaneById(int id);
        Task<IEnumerable<DestinationModelBase>> GetPassengerById(int id);
        Task<DestinationModelBase> Insert(DestinationModelCreate model);
        Task<DestinationModelBase> Update(DestinationModelUpdate model);
        Task<bool> Delete(int id);
    }
}
