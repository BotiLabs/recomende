using System;
using System.Collections.Generic;

namespace Eudora.Api.Utils
{
    public class DataBuilder
    {
        public static string ImageUrl(string categoria)
        {
            var random = new Random().Next(1, 4);
            switch (categoria)
            {
                case "ACESSÓRIOS":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/acessorio{random}.png";
                case "BANHO":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/banho{random}.png";
                case "BARBA":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/barba{random}.png";
                case "CABELOS":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/cabelos{random}.png";
                case "CORPO":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/corpo{random}.png";
                case "DESODORANTES":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/desodorante{random}.png";
                case "EMBALAGENS":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/embalagem{random}.png";
                case "MAQUIAGEM":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/maquiagem{random}.png";
                case "MATERIAL DE APOIO":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/material{random}.png";
                case "PERFUMARIA":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/perfume{random}.png";
                case "ROSTO":
                    return $"https://grupo09storage.blob.core.windows.net/grupo9/rosto{random}.png";
                default:
                    return "https://grupo09storage.blob.core.windows.net/grupo9/_eudora.png";
            }
        }

        public static string WomanImageUrl()
        {
            var random = new Random().Next(1, 11);
            return $"https://grupo09storage.blob.core.windows.net/grupo9/mulher{random}.jpg";
        }

        public static string Categoria()
        {
            var categorias = new List<string>{
                "ACESSÓRIOS", "BANHO", "BARBA", "CABELOS", "CORPO", "DESODORANTES",
                "EMBALAGENS", "MAQUIAGEM", "MATERIAL DE APOIO", "PERFUMARIA", "ROSTO"};

            var random = new Random().Next(0, categorias.Count);
            return categorias[random];
        }

        public static decimal Valor()
        {
            decimal value = (decimal)new Random().NextDouble() * 99;
            return decimal.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        public static string Telefone()
        {
            var telefones = new List<string> { "+5519981411694" , "+5541992549495" };
            var random = new Random().Next(0, telefones.Count);
            return telefones[random];
        }
    }
}
