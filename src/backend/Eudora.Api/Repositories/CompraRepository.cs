using Eudora.Api.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Repositories
{
    public class CompraRepository<T> : BaseRepository<Compra>, ICompraRepository
    {
        public CompraRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Compra>> GetLast(int lastCount, int CodRepresentante)
        {
            var sqlParams = new { CodRepresentante };
            return await Query($"SELECT TOP {lastCount} * FROM [dbo].[compras] WHERE COD_REVENDEDOR = @CodRepresentante ORDER BY DAT_CAPTACAO DESC", sqlParams);
        }      
    }
}
