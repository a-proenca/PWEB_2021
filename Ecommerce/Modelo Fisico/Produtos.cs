//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce.Modelo_Fisico
{
    using System;
    using System.Collections.Generic;
    
    public partial class Produtos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produtos()
        {
            this.Produtos1 = new HashSet<Produtos>();
        }
    
        public int IdProduto { get; set; }
        public string NomeP { get; set; }
        public float PrecoP { get; set; }
        public int QuantP { get; set; }
        public string OrigemP { get; set; }
        public string EstadoP { get; set; }
        public string CategoriaP { get; set; }
        public float DesctP { get; set; }
        public Nullable<int> Produto_IdProduto { get; set; }
        public Nullable<int> Pedidos_IdPd { get; set; }
        public string Description { get; set; }
        public string IdEmpresa { get; set; }
        public string Empresa_Id { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Pedidos Pedidos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produtos> Produtos1 { get; set; }
        public virtual Produtos Produtos2 { get; set; }
    }
}
