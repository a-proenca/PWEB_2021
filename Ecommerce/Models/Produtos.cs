using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Produtos
    {
        [Key]
        [Display(Name = "ID Produto")]
        public int IdProduto { get; set; }

        [Display(Name = "Nome Produto")]
        [Required(ErrorMessage = "Nome do Produto em falta!")]
        [DataType(DataType.Text), MaxLength(60)]
        public string NomeP { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Insira um valor positivo")]
        [Display(Name = "Preço")]
        public float PrecoP { get; set; }

        [Display(Name = "Quantidade Produto")]
        [Required(ErrorMessage = "Insira a quantidade do produto!")]
        public int QuantP { get; set; }
       
        public string EstadoP { get; set; }

        [Display(Name = "Origem do Produto")]
        [Required(ErrorMessage = "Origem do produto em falta!")]
        [DataType(DataType.Text), MaxLength(60)]

        public string OrigemP { get; set; }

        [Display(Name = "Categoria do Produto")]
        [Required(ErrorMessage = "Categoria do produto em falta!")]
        [DataType(DataType.Text), MaxLength(60)]

        public string CategoriaP { get; set; }


   
        [Display(Name = "Desconto do Produto")]
        public float DesctP { get; set; }

        public bool Desc { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
        //dividir o desconto por 100 para depois multiplicar pelo preço original em vez de trabalhar com %

        
        public string Id_Empresa { get; set; }
        [ForeignKey("Id_Empresa")]

        public virtual Empresas Empresa { get; set; }

        public Produtos()
        {
            DesctP = 0;

            EstadoP = "Em Stock";
    
        }



    }
}