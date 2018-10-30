using Eudora.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Services
{
    public interface IRepresentanteService
    {
        Task<IEnumerable<Representante>> GetAll();
        Task<RepresentanteDashboard> GetDashboard(int codRepresentante);
        Task<bool> Populate();
    }
}
