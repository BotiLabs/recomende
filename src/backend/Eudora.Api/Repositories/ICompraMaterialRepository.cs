using Eudora.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public interface ICompraMaterialRepository
    {
        Task<IEnumerable<CompraMaterial>> GetUltimosProdutosComprados(int CodRepresentante);
    }
}
