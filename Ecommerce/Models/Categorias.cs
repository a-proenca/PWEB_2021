using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Categorias
    {   
        [Key]
        public int IdCat { get; set; }
        public string NomeCat { get; set; }
    }
}