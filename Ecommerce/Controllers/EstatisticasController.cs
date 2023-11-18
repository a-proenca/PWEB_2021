using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class EstatisticasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static List<SelectListItem> categoriasprodutos = new List<SelectListItem>(){
   
    };


        // GET: Estatisticas
        public ActionResult estatisticaprodutos()
        {
    



          Stat numero = new Stat();
            numero.qtd=    db.Pedido.GroupBy(y => y.Id_Cliente).Count();



            return View(numero);
        }

        public ActionResult StatNMV()
        {
            decimal nmv;
            List<Pedidos> pedido = db.Pedido.ToList();

            int totalV = db.Pedido.Count();

            nmv = totalV / 2; //Esta media mensal esta feita so para 2 meses visto que
            //nao ha dados temporais suficientes para estar a fazer para mais que 2 meses
            //Nota: Incluir Relatorio!!!!


            Stat conta1 = new Stat(nmv);
            conta1.tipo = db.Pedido.Count().ToString();

            return View(conta1);


        }


        public ActionResult StatNMC()
        {
            decimal nmv;
            List<Pedidos> pedido = db.Pedido.ToList();
            List<Stat> stat = new List<Stat>();

            Stat stati = new Stat();
            var empresas=  db.Empresas.ToList();


            foreach(var l in empresas) { 
            foreach(var lin in pedido)
            {
                    stati.qtd = stati.qtd+ lin.Produto.Where(x => x.Id_Empresa == l.Id).Count();
                    stati.tipo = l.Nome;

                  

            }
                stat.Add(stati);
            }
            return View(stat);
        }


        public ActionResult Index()
        {

            return View();
        }


    }
}