using System.Collections.Generic;

namespace Eudora.Api.Models
{
    public class RepresentanteDashboard
    {
        public Representante Representante { get; set; }
        public IEnumerable<CompraCategoria> Categorias { get; set; }
        public IEnumerable<Compra> UltimasCompras { get; set; }
        public IEnumerable<Recomendacao> Recomendacoes { get; set; }
    }
}
