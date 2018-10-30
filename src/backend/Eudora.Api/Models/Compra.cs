using Eudora.Api.Utils;
using System;

namespace Eudora.Api.Models
{
    public class Compra
    {
        public int Cod_Material { get; set; }
        public string Descricao { get; set; }
        public int Vlr_Qtd_Unidades { get; set; }
        public DateTime Dat_Captacao { get; set; }
        public string Categoria { get; set; }
        public string ImagemProduto
        {
            get
            {
                return DataBuilder.ImageUrl(Categoria);
            }
        }
    }
}
