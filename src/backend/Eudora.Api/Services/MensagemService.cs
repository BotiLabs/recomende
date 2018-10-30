using Eudora.Api.Models;
using Eudora.Api.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eudora.Api.Utils;
using System.Linq;

namespace Eudora.Api.Services
{
    public class MensagemService : IMensagemService
    {
        private readonly IConfiguration _configuration;
        private readonly IMensagemRepository _mensagemRepository;

        public MensagemService(IConfiguration configuration, IMensagemRepository mensagemRepository)
        {
            _configuration = configuration;
            _mensagemRepository = mensagemRepository;
        }

        public async Task<IEnumerable<Mensagem>> GetAll()
        {
            return await _mensagemRepository.GetAll();
        }

        public async Task Add(Mensagem mensagem)
        {
            await _mensagemRepository.Add(mensagem);            
        }
    }
}
