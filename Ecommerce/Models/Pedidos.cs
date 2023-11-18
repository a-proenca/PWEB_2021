using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Ecommerce.Models
{
    //[Table("Pedidos")]
    public class Pedidos
    {

        [Key]
        public int IdPd { get; set; }

        public DateTime DataEntrega { get; set; } //quando o pedido for entregue obter data de entrega auto

        [Display(Name = "Data Pedido")]
        public DateTime DataPd { get; set; }

        public string EstadoPd { get; set; } //quando pedido confirmado passar a "em distribuiçao" , 
                                             //quando for entregue passar a "Concluido"


        [Display(Name = "Levantamento")]
        [Required(ErrorMessage = "Método de Levantamento em falta!")]
        public string Levantamento { get; set; }
        //T = levantar na loja , F= entregar em casa

        [Display(Name = "Morada")]
        [Required(ErrorMessage = "Morada em falta!")]
        public string MoradaPd { get; set; }
        //Se o cliente tiver ja colocado a morada , automaticamente MoradaPD já vai ter a informação

        [Display(Name = "Preço")]
        public float ValorPd { get; set; }
        //somar todos os produtos add ao carrinho/pedido

        [Display(Name = "Método de pagamento")]
        [Required(ErrorMessage = "Metodo de pagamento em falta!")]
        public string MetodoPag { get; set; }

        public string Id_Cliente { get; set; }
        [ForeignKey("Id_Cliente")]

        public virtual Clientes Clientes { get; set; }

        public virtual ICollection<Produtos> Produto { get; set; }

        public Pedidos()
        {
            DataPd = System.DateTime.Now;
            EstadoPd = "Pendente";

        }


    }
}