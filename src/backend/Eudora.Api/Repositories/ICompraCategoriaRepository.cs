using Eudora.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public interface ICompraCategoriaRepository
    {
        Task<IEnumerable<CompraCategoria>> GetCategoriasMaisCompradas(int CodRepresentante);
    }
}
