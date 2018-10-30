using Eudora.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public interface IRepresentanteRepository
    {
        Task<Representante> GetById(int id);
        Task<IEnumerable<Representante>> GetAll();
        Task Add(Representante representante);
        Task SetContatoRealizado(int id);
        Task Truncate();
    }
}
