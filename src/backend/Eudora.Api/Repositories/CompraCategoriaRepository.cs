using Eudora.Api.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public class CompraCategoriaRepository<T> : BaseRepository<CompraCategoria>, ICompraCategoriaRepository
    {
        public CompraCategoriaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<CompraCategoria>> GetCategoriasMaisCompradas(int CodRepresentante)
        {
            var sqlParams = new { CodRepresentante };
            return await Query("SELECT SUM(VLR_PRATICADO) AS ValorTotalTxt, CATEGORIA FROM[dbo].[compras] WHERE COD_REVENDEDOR = @CodRepresentante GROUP BY CATEGORIA", sqlParams);
        }
    }
}
