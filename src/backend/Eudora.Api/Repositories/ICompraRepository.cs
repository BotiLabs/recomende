using Eudora.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public interface ICompraRepository
    {
        Task<IEnumerable<Compra>> GetLast(int lastCount, int CodRepresentante);
    }
}
