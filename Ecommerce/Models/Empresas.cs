using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Ecommerce.Models
{
    public class Empresas : ApplicationUser
    {
 
        [Display(Name = "Indique a morada da Empresa")]
        public string MoradaEmp { get; set; }

        [Display(Name = "Indique o número de identificação fiscal da Empresa")]
        public int NIFEmp { get; set; }

        public Int32? ImageID { get; set; }

        [ForeignKey("ImageID")]
        public virtual Image Imagem { get; set; }
        
        public virtual List<Produtos> Produtos { get; set; }
        public virtual List<Funcionários> Funcionários { get; set; }
    }
}