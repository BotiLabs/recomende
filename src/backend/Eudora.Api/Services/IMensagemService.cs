using Eudora.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Services
{
    public interface IMensagemService
    {
        Task<IEnumerable<Mensagem>> GetAll();
        Task Add(Mensagem mensagem);
    }
}
