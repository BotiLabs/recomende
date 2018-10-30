using Eudora.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public interface IMensagemRepository
    {
        Task<IEnumerable<Mensagem>> GetAll();
        Task Add(Mensagem representante);
    }
}
