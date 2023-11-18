using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Ecommerce.Models;

namespace Ecommerce.Models
{
    public class Estatisticas
    {
        public string tipo { get; set; }
        public int qtd { get; set; }


        public Estatisticas(string tipo, int qtd)
        {
            this.tipo = tipo;
            this.qtd = qtd;
        }
    }
}