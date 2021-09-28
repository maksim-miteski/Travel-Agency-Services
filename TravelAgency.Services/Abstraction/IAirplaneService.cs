using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Airplane;

namespace TravelAgency.Services.Abstraction
{
    public interface IAirplaneService
    {
        Task<IEnumerable<AirplaneModelBase>> Get();
        Task<AirplaneModelExtended> Get(int id);
        Task<AirplaneModelBase>  Insert(AirplaneModelCreate model);
        Task<AirplaneModelBase>Update(AirplaneModelUpdate model);
        Task<bool> Delete(int id);
    }
}
