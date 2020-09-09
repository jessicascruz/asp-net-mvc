using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Projeto01.Areas.Seguranca.Data;
using Projeto01.Infraestrutura;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Areas.Seguranca.Controllers
{
    public class PapelAdminController : Controller
    {
        // GET: Seguranca/PapelAdmin
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create([Required] string nome)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = RoleManager.Create(new Papel(nome));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(nome);
        }

        // Get
        public ActionResult Edit(string id)
        {
            Papel papel = RoleManager.FindById(id);
            string[] memberIDs = papel.Users.Select(x => x.UserId).ToArray();

            IEnumerable<Usuario> membros = UserManager.Users.Where(x => memberIDs.Any(YieldAwaitable => YieldAwaitable == x.Id));
            IEnumerable<Usuario> naoMembros = UserManager.Users.Except(membros);

            return View(new PapelEditModel
            {
                Papel = papel,
                Membros = membros,
                NaoMembros = naoMembros
            });
        }

        [HttpPost]
        public ActionResult Edit(PapelModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsParaAdicionar ?? new string[] { })
                {
                    result = UserManager.AddToRole(userId, model.NomePapel);
                    if (!result.Succeeded)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                foreach (string userId in model.IdsParaRemover ?? new string[] { })
                {
                    result = UserManager.RemoveFromRole(userId, model.NomePapel);
                    if (!result.Succeeded)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            return View();
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private GerenciadorUsuario UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }

        private GerenciadorPapel RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorPapel>();
            }
        }

    }
}