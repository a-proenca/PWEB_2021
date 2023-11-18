using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Ecommerce.Controllers
{
    public class FuncionáriosController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        //private ApplicationDbContext context = new ApplicationDbContext();
        private ApplicationDbContext db = new ApplicationDbContext();

        public FuncionáriosController()
        {
        }

        public FuncionáriosController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Funcionários
        public ActionResult Index(string sortOrder, string SearchString)
        {
                ViewBag.NomeEmp = String.IsNullOrEmpty(sortOrder) ? "Nome" : "";
            var idd = User.Identity.GetUserId();

            var ola = (from s in db.Funcionários
                      where s.EmpresaID == idd
                      select s).ToList();

            return View(ola); 
        }

        //
        // GET: /Funcionarios/Create
        [AllowAnonymous]
        public ActionResult Register()
        {
  
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var Id = User.Identity.GetUserId();
            ApplicationUser Utilizador = null;
            if (ModelState.IsValid)
            {
      
                Utilizador = new Funcionários { Nome = model.Nome, UserName = model.Email, Email = model.Email, PhoneNumber = model.Telefone, NIFF = model.Nif, EmpresaID=Id};
                var result = await UserManager.CreateAsync(Utilizador, model.Password);
                if (result.Succeeded)
                {
                    var UserRole = UserManager.AddToRoles(Utilizador.Id, "Funcionário");

                    return RedirectToAction("Index", "Funcionários");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
    #endregion
}