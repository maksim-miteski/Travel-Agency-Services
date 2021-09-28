using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Passenger;

namespace TravelAgency.Services.Abstraction
{
    public interface IPassengerService
    {
        Task<IEnumerable<PassengerModelBase>> Get();
        Task<PassengerModelExtended> Get(int id);
        Task<PassengerModelBase> Insert(PassengerModelCreate model);
        Task<PassengerModelBase> Update(PassengerModelUpdate model);
        Task<bool> Delete(int id);
    }
}
