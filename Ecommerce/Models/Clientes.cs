using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Ecommerce.Models
{
    public class Clientes : ApplicationUser
    {
        [Display(Name = "Insira a sua morada!")]
        public string MoradaC { get; set; }
        
        [Display(Name = "Indique o seu número de identificação fiscal")]

        [DataType(DataType.Text)]
        public int NIFC { get; set; }

        public virtual List<Pedidos> Pedidos { get; set; }
    }
}