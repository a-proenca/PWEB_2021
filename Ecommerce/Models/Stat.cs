using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Ecommerce.Models;

namespace Ecommerce.Models
{
    public class Stat
    {
        public string tipo { get; set; }
        public decimal qtd { get; set; }


        public Stat(decimal qtd=0)
        {
            //this.tipo = tipo;
            this.qtd = qtd;
        }
    }
}