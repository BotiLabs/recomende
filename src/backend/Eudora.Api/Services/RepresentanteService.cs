using Eudora.Api.Models;
using Eudora.Api.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eudora.Api.Utils;
using System.Linq;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Eudora.Api.Services
{
    public class RepresentanteService : IRepresentanteService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepresentanteRepository _representanteRepository;
        private readonly ICompraRepository _compraRepository;
        private readonly ICompraCategoriaRepository _compraCategoriaRepository;
        private readonly ILogger<RepresentanteService> _logger;

        public RepresentanteService(IConfiguration configuration, IRepresentanteRepository representanteRepository,
            ICompraRepository compraRepository, ICompraCategoriaRepository compraCategoriaRepository, ILogger<RepresentanteService> logger)
        {
            _configuration = configuration;
            _representanteRepository = representanteRepository;
            _compraRepository = compraRepository;
            _compraCategoriaRepository = compraCategoriaRepository;
            _logger = logger;
        }

        public async Task<RepresentanteDashboard> GetDashboard(int codRepresentante)
        {
            var representanteTask = _representanteRepository.GetById(codRepresentante);
            var categoriasTask = _compraCategoriaRepository.GetCategoriasMaisCompradas(codRepresentante);
            var comprasTask = _compraRepository.GetLast(5, codRepresentante);
            var recomendacoesTask = GetRecomendacoes(codRepresentante);
            var contatoRealizadoTask = _representanteRepository.SetContatoRealizado(codRepresentante);

            await Task.WhenAll(representanteTask, categoriasTask, comprasTask, recomendacoesTask, contatoRealizadoTask);

            if (representanteTask.Result != null && representanteTask.IsCompletedSuccessfully && 
                categoriasTask.IsCompletedSuccessfully && comprasTask.IsCompletedSuccessfully &&
                recomendacoesTask.IsCompletedSuccessfully && contatoRealizadoTask.IsCompletedSuccessfully)
            {
                var recomendacoes = new List<Recomendacao>();
                if (recomendacoesTask.Result != null)
                {
                    foreach (var item in recomendacoesTask.Result)
                    {
                        recomendacoes.Add(new Recomendacao {
                            Cod_Material = item.Code,
                            Descricao = item.Description,
                            Categoria = item.Category,
                            Valor = item.Price
                        });
                    }
                }

                return new RepresentanteDashboard {
                    Representante = representanteTask.Result,
                    Categorias = categoriasTask.Result.OrderByDescending(o => o.ValorTotal),
                    UltimasCompras = comprasTask.Result,
                    Recomendacoes = recomendacoes
                };

            }
            return null;
        }

        public async Task<IEnumerable<Representante>> GetAll()
        {
            return await _representanteRepository.GetAll();
        }

        public async Task<bool> Populate()
        {
            await _representanteRepository.Truncate();

            for (var i = 1; i <= 5000; i++)
            {
                var nome = new NameGenerator().Create();
                var representante = new Representante { Id = i, Nome = nome };
                await _representanteRepository.Add(representante);
            }

            return true;            
        }

        private async Task<IList<ProdutoRecomendacao>> GetRecomendacoes(int codRepresentante)
        {
            var httpClient = new HttpClient();

            var url = _configuration.GetSection(Configurations.WebServicesSection).GetValue<string>(Configurations.RecommendSystemSvc);
            var urlBuilder = new UriBuilder(url) { Path = $"revendedor/{codRepresentante}" };

            try
            {
                var result = await httpClient.GetAsync(urlBuilder.Uri);

                if (result.IsSuccessStatusCode)
                {
                    string content = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<ProdutoRecomendacao>>(content);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while calling recommendarion system");
            }

            return null;
        }
    }
}
