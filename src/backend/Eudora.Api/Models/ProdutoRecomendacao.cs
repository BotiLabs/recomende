using Eudora.Api.Utils;
using System;

namespace Eudora.Api.Models
{
    public class ProdutoRecomendacao
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
