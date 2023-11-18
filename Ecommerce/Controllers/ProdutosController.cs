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
    public class ProdutosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produtos
        public ActionResult Index(string sortOrder, string searchString)
        {
            
            ViewBag.OrdemDef = sortOrder;
            ViewBag.OrdemNome = string.IsNullOrEmpty(sortOrder) ? "NomeP_desc" : "";
            ViewBag.OrdemCategoria = sortOrder == "" ? "CategoriaP_desc" : "CategoriaP";
            var produtos = from s in db.Produtos
                           select s;

            foreach(var li in produtos)
            {
                if (li.QuantP<1)
                {
                    li.EstadoP = "Out of Stock";
                }
            }
 

            if (!String.IsNullOrEmpty(searchString))
            {
                produtos = produtos.Where(s => s.NomeP.Contains(searchString) || s.CategoriaP.Contains(searchString));
            }

            switch (sortOrder) 
            {
                case "NomeP_desc":
                    produtos = produtos.OrderByDescending(s => s.NomeP);
                    break;
                case "CategoriaP":
                    produtos = produtos.OrderBy(s => s.CategoriaP);
                    break;
                case "CategoriaP_desc":
                    produtos = produtos.OrderBy(s => s.CategoriaP);
                    break;
                default:
                    produtos = produtos.OrderBy(s => s.NomeP);
                    break;
            }
      
            return View(produtos.Where(x=>x.OrigemP!="1").ToList());
        }

        public ActionResult ProdutosEmp(string sortOrder, string searchString)
        {
            ViewBag.OrdemDef = sortOrder;
            ViewBag.OrdemNome = string.IsNullOrEmpty(sortOrder) ? "NomeP_desc" : "";
            ViewBag.OrdemCategoria = sortOrder == "" ? "CategoriaP_desc" : "CategoriaP";

            var id = User.Identity.GetUserId();


            var utilizador = db.Users.Find(id);

            var prod = from s in db.Produtos
                       where s.Id_Empresa == utilizador.Id
                           select s;

            foreach (var li in prod)
            {
                if (li.QuantP < 1)
                {
                    li.EstadoP = "Out of Stock";
                }
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                prod = prod.Where(s => s.NomeP.Contains(searchString) || s.CategoriaP.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "NomeP_desc":
                    prod = prod.OrderByDescending(s => s.NomeP);
                    break;
                case "CategoriaP":
                    prod = prod.OrderBy(s => s.CategoriaP);
                    break;
                case "CategoriaP_desc":
                    prod = prod.OrderBy(s => s.CategoriaP);
                    break;
                default:
                    prod = prod.OrderBy(s => s.NomeP);
                    break;
            }


            

            return View(prod.Where(x=>x.OrigemP!="1").ToList());
        }

        public ActionResult ProdutosFunc(string sortOrder, string searchString)
        {
            ViewBag.OrdemDef = sortOrder;
            ViewBag.OrdemNome = string.IsNullOrEmpty(sortOrder) ? "NomeP_desc" : "";
            ViewBag.OrdemCategoria = sortOrder == "" ? "CategoriaP_desc" : "CategoriaP";

            var id = User.Identity.GetUserId();


            var func = db.Funcionários.Find(id);

            var prod = from s in db.Produtos
                       where s.Id_Empresa == func.EmpresaID
                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                prod = prod.Where(s => s.NomeP.Contains(searchString) || s.CategoriaP.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "NomeP_desc":
                    prod = prod.OrderByDescending(s => s.NomeP);
                    break;
                case "CategoriaP":
                    prod = prod.OrderBy(s => s.CategoriaP);
                    break;
                case "CategoriaP_desc":
                    prod = prod.OrderBy(s => s.CategoriaP);
                    break;
                default:
                    prod = prod.OrderBy(s => s.NomeP);
                    break;
            }

            return View(prod.Where(x => x.OrigemP != "1").ToList());
        }



        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtos produtos = db.Produtos.Find(id);
            if (produtos == null)
            {
                return HttpNotFound();
            }
            return View(produtos);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {

            ViewBag.Categorias = new SelectList(db.Categorias, "NomeCat", "NomeCat");
            List<string> origemProd = new List<string> { "Nacional", "Importado" };
            ViewBag.OrigemP = new SelectList(origemProd,"Text");
            
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduto,NomeP,PrecoP,QuantP,EstadoP,OrigemP,CategoriaP,DesctP,Description,IdEmpresa")] Produtos produtos)
        {
           string id=User.Identity.GetUserId();

            ViewBag.Categorias = new SelectList(db.Categorias, "NomeCat", "NomeCat");
            List<string> origemProd = new List<string> { "Nacional", "Importado" };
            ViewBag.OrigemP = new SelectList(origemProd, "Text");
           

            produtos.Id_Empresa = id;

            if (ModelState.IsValid && produtos.QuantP>1)
            { 
                
                db.Produtos.Add(produtos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produtos);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtos produtos = db.Produtos.Find(id);
            if (produtos == null)
            {
                return HttpNotFound();
            }
            return View(produtos);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduto,NomeP,PrecoP,QuantP,EstadoP,OrigemP,CategoriaP,DesctP,Description")] Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produtos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produtos);
        }



        public ActionResult EditDesconto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtos produtos = db.Produtos.Find(id);
            if (produtos == null)
            {
                return HttpNotFound();
            }
            return View(produtos);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDesconto([Bind(Include = "IdProduto,NomeP,PrecoP,QuantP,EstadoP,OrigemP,CategoriaP,DesctP,Description")] Produtos produtos)
        {

            produtos.Desc = true;
            var valor = produtos.DesctP / 100;

            produtos.PrecoP =  produtos.PrecoP * (1- valor);

            if (ModelState.IsValid)
            {
                db.Entry(produtos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produtos);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtos produtos = db.Produtos.Find(id);
            if (produtos == null)
            {
                return HttpNotFound();
            }
            return View(produtos);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produtos produtos = db.Produtos.Find(id);
            db.Produtos.Remove(produtos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Adicionar(int id)
        {
            if (Session["Carrinho"] != null)
            {
                List<Produtos> listaprodutos = (List<Produtos>)Session["Carrinho"];

                List<Produtos> ListaNova = listaprodutos;
                var resultado = db.Produtos.Find(id);

                var valor = listaprodutos.Where(x => x.IdProduto == id).Select(x => x.QuantP).FirstOrDefault();
                valor++;
                resultado.QuantP = valor;
                if (listaprodutos.Where(x => x.IdProduto == id).Count() > 0)
                {

                    
                    ListaNova.RemoveAll(x => x.IdProduto == id);
                    ListaNova.Add(resultado);
                }
                else
                {
                    ListaNova.Add(resultado);

                }
               

                listaprodutos = ListaNova;
                Session["Carrinho"] = listaprodutos;
                Session["Valor"] = listaprodutos.Count();
            }
            else
            {
                List<Produtos> listaprodutos = new List<Produtos>();
                var produto = db.Produtos.Find(id);
                produto.QuantP = 1;
                listaprodutos.Add(produto);
                Session["Carrinho"] = listaprodutos;
                Session["Valor"] = listaprodutos.Count();
            }

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
