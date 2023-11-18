using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Compra()
        {

            List<Produtos> Carrinho = (List<Produtos>)Session["Carrinho"];
          float  valortotal = 0;

            foreach(var lin in Carrinho)
            {

                valortotal = valortotal + (lin.PrecoP * lin.QuantP);
            }



            if (Carrinho.Count > 0)
            {
                ViewBag.Total = Carrinho.Count;
                ViewBag.PrecoFinal = valortotal;
                Session["ValorCompra"] = valortotal;
             }
            return View(Carrinho);
        }

        public ActionResult Delete(int? id)
        {


            List<Produtos> produtos = (List<Produtos>)Session["Carrinho"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtos Produtosfinal = produtos.Where(x=>x.IdProduto==id).FirstOrDefault();


            produtos.Remove(Produtosfinal);


            Session["Carrinho"] = produtos;


            if(produtos==null || produtos.Count() == 0)
            {

                Session["ValorCompra"] = null;
                Session["Carrinho"] = null;

                return RedirectToAction("Index", "Produtos");
            }
            else
            {
                float valortotal = 0;
                foreach (var lin in produtos)
            {

                    valortotal = valortotal + (lin.PrecoP * lin.QuantP);
                }

                Session["ValorCompra"] = valortotal;


            }
            return View(produtos);
            
        }
    }
}