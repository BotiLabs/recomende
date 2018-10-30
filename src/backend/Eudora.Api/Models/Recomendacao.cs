using Eudora.Api.Utils;

namespace Eudora.Api.Models
{
    public class Recomendacao
    {
        public int Cod_Material { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public string ImagemProduto
        {
            get
            {
                return DataBuilder.ImageUrl(Categoria);
            }
        }
    }
}
