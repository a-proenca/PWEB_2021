using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Ecommerce.Models
{
    public class Funcionários : ApplicationUser
    {


        [Required]
        [Display(Name = "NIF")]
        public int NIFF { get; set; }

        [Required]
        public string EmpresaID { get; set; }
        [ForeignKey("EmpresaID")]

        public virtual Empresas Empresa { get; set; }
    }
    
}