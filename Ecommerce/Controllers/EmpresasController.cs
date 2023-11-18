using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Microsoft.AspNet.Identity;

namespace Ecommerce.Controllers
{
    
    public class EmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Empresas
        public ActionResult Index(string sortOrder, string SearchString)
        {
            ViewBag.NomeEmp = String.IsNullOrEmpty(sortOrder) ? "Nome" : "";

            var empresas = db.Empresas.Include(s => s.Imagem).ToList();

            if (!String.IsNullOrEmpty(SearchString))
            {
                
                empresas = db.Empresas.Include(s=>s.Imagem).Where(s => s.Nome==SearchString).ToList();
            }


            return View(empresas);
        }

        //GEt:ACTIMGS
        [HttpGet]
        public ActionResult Image()
        {
            return View();
        }

        //POST:ACTIMGS
        [HttpPost]
        [Authorize(Roles = "Empresa")]
        [ValidateAntiForgeryToken]
        public ActionResult Image(Image img)
        {
            string empresaid = User.Identity.GetUserId();

            var empresa = db.Empresas.Find(empresaid);
            string extension = Path.GetExtension(img.ImageFile.FileName);
            if (extension.ToUpper() != ".PNG" && extension.ToUpper() != ".JPEG" && extension.ToUpper() != ".JPG")
            {
                ModelState.AddModelError("", "Imagem tem de estar nos formatos PNG,JPEG,JPG.");
            }
            img.Title =  empresa.Nome+empresa.Id + extension;
            img.ImagePath = "~/Image/" + img.Title;
            img.Title = Path.Combine(Server.MapPath("~/Image/"), img.Title);
            img.ImageFile.SaveAs(img.Title);

            var listaempresas = db.Image.Where(x => x.Title == img.Title).ToList();

            foreach(var lin in listaempresas)
            {
                db.Image.Remove(lin);
                db.SaveChanges();
            }

            db.Image.Add(img);
            db.SaveChanges();

            var imgf = db.Image.Where(x => x.Title.Contains(empresa.Nome)).Where(x => x.ImagePath.Contains("Image/")).OrderByDescending(x => x.ImageID).FirstOrDefault();

            empresa.ImageID = imgf.ImageID;
            db.Entry(empresa).CurrentValues.SetValues(empresa);
            db.SaveChanges();

            return View();
        }
    }

 
}
