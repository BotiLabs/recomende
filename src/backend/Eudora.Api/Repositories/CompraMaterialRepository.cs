using Eudora.Api.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public class CompraMaterialRepository<T> : BaseRepository<CompraMaterial>, ICompraMaterialRepository
    {
        public CompraMaterialRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<CompraMaterial>> GetUltimosProdutosComprados(int CodRepresentante)
        {
            var sqlParams = new { CodRepresentante };
            return await Query("SELECT DISTINCT TOP 5 COD_MATERIAL, COD_CICLO FROM [dbo].[compras] where COD_REVENDEDOR = @CodRepresentante ORDER BY COD_CICLO desc", sqlParams);
        }
    }
}
