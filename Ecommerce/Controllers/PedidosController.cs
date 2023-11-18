using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Microsoft.AspNet.Identity;

namespace Ecommerce.Controllers
{
    public class PedidosController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pedidos
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();

            var utilizador = db.Users.Find(id);

            return View();
        }

        public ActionResult PedidosCliente()
        {
            var id = User.Identity.GetUserId();
            var pedidos = db.Pedido.Where(x => x.Id_Cliente == id).ToList();

            return View(pedidos);
        }

        
        public ActionResult Pedidos()
        {


          var pedidos=  db.Pedido.Where(x => x.DataEntrega < DateTime.Now).ToList();

        
            foreach(var lin in pedidos)
            {


                lin.EstadoPd = "Entregue";
                db.Entry(lin).CurrentValues.SetValues(lin);

                db.SaveChanges();
            }

            var funcionario = db.Funcionários.Find(User.Identity.GetUserId());

            var pedidosf = db.Pedido.Where(x => x.Produto.Where(y => y.Id_Empresa == funcionario.EmpresaID).Count() > 0).ToList();

            return View(pedidosf.ToList());
        }



        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedido.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }

            var funcionario = db.Funcionários.Find(User.Identity.GetUserId());


           var produtosf = pedidos.Produto.Where(x => x.Id_Empresa == funcionario.EmpresaID).ToList();

            pedidos.Produto = produtosf;

            return View(pedidos);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {

            Pedidos pedido = new Pedidos();

            pedido.DataPd = DateTime.Now;
            pedido.DataEntrega = DateTime.Now.AddDays(2);
            pedido.Produto = (List<Produtos>)Session["Carrinho"];
            List<string> Levantamento = new List<string> { "Em Loja", "Ponto de Recolha", "Para Casa" };
            ViewBag.Levantamento = new SelectList(Levantamento, "Text");
            List<string> MetodoPag = new List<string> { "PayPal", "Cartão de Crédito", "Transferência Bancária" };
            ViewBag.MetodoPag = new SelectList(MetodoPag, "Text");
            pedido.ValorPd = (float)Session["ValorCompra"];
            return View(pedido);
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPd,DataEntrega,DataPd,EstadoPd,TipoPd,MoradaPd,Levantamento,ValorPd,MetodoPag")] Pedidos pedidos)
        {
            
            var id = User.Identity.GetUserId();
    
            List<string> Levantamento = new List<string> { "Em Loja", "Ponto de Recolha", "Para Casa" };
            ViewBag.Levantamento = new SelectList(Levantamento, "Text");
            List<string> MetodoPag = new List<string> { "PayPal", "Cartão de Crédito", "Transferência Bancária" };
            pedidos.DataEntrega = DateTime.Now.AddDays(2);
            ViewBag.MetodoPag = new SelectList(MetodoPag, "Text");
            List<Produtos> produtoslist = (List<Produtos>)Session["Carrinho"];
            List<Produtos> ListaClient = (List<Produtos>)Session["Carrinho"];

            foreach (var lin in produtoslist)
            {

                
                lin.OrigemP = "1";
            }
            pedidos.Produto = produtoslist;


            pedidos.Id_Cliente = id;
            
            if (ModelState.IsValid)
            {
                db.Pedido.Add(pedidos);
                db.SaveChanges();

                Session["Carrinho"] = null;


                foreach(var lina in ListaClient)
                {

                   var produto = db.Produtos.Where(x => x.NomeP == lina.NomeP && x.OrigemP!="1").FirstOrDefault();

                    produto.QuantP = produto.QuantP - lina.QuantP;
                    db.Entry(produto).CurrentValues.SetValues(produto);

                    db.SaveChanges();

                }
                return RedirectToAction("PedidosCliente");
            }

            return View(pedidos);
        }


        public ActionResult ConfEntrega(int? id)
        {


           var pedido= db.Pedido.Find(id);

            foreach(var lin in pedido.Produto)
            {
                var produto = db.Produtos.Where(x => x.IdProduto == lin.IdProduto).FirstOrDefault();
                int total = produto.QuantP;
                if ((total - lin.QuantP) >= 0)
                {

                    produto.QuantP = total - lin.QuantP;

                    db.Entry(produto).CurrentValues.SetValues(produto);

                    db.SaveChanges();




                    pedido.EstadoPd = "Enviado";
                    db.Entry(pedido).CurrentValues.SetValues(pedido);

                    db.SaveChanges();
                }
                else
                {
                    pedido.EstadoPd = "Em rutura";
                    db.Entry(pedido).CurrentValues.SetValues(pedido);

                    db.SaveChanges();
                }
            }


            




            return RedirectToAction("Pedidos");
        }
        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedido.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPd,DataEntrega,DataPd,EstadoPd,TipoPd,MoradaPd,ValorPd,MetodoPag")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pedidos);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedido.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedidos pedidos = db.Pedido.Find(id);
            db.Pedido.Remove(pedidos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
