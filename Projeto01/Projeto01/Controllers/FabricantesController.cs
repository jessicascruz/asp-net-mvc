using Projeto01.Contexts;
using Projeto01.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Projeto01.Controllers
{
    public class FabricantesController : Controller
    {
        private EFContext context = new EFContext();

        private static IList<Fabricante> fabricantes = new List<Fabricante>()
        {
            new Fabricante()
            {
                FabricanteId = 1,
                Nome = "Dell"
            },
            new Fabricante()
            {
                FabricanteId = 2,
                Nome = "Samsung"
            },
            new Fabricante()
            {
                FabricanteId = 3,
                Nome = "Vaio"
            },
            new Fabricante()
            {
                FabricanteId = 4,
                Nome = "Acer"
            },
            new Fabricante()
            {
                FabricanteId = 5,
                Nome = "HP"
            },
        };


        // GET: Fabricantes
        public ActionResult Index()
        {
            // return View(fabricantes.OrderBy(c => c.Nome));
            return View(context.Fabricantes.OrderBy(c => c.Nome));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            //fabricante.FabricanteId =
            //    fabricantes.Select(m => m.FabricanteId).Max() + 1;

            //fabricantes.Add(fabricante);
            context.Fabricantes.Add(fabricante);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            //return View(fabricantes.Where(
            //    m => m.FabricanteId == id).First());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = context.Fabricantes.Find(id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            //fabricantes.Remove(
            //    fabricantes.Where(m => m.FabricanteId == fabricante.FabricanteId).First());

            //fabricantes.Add(fabricante);

            //return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                context.Entry(fabricante).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        public ActionResult Details(long? id)
        {
            // return View(fabricantes.Where(
            //    m => m.FabricanteId == id).First());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = context.Fabricantes.Where(f => f.FabricanteId == id).Include("Produtos.Categoria").First();

            if (fabricante == null)
            {
                return HttpNotFound();
            }

            return View(fabricante);
        }

        public ActionResult Delete(long? id)
        {
            //return View(fabricantes.Where(
            //    c => c.FabricanteId == id).First());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Fabricante fabricante = context.Fabricantes.Find(id);
            
            if (fabricante == null)
            {
                return HttpNotFound();
            }

            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            //fabricantes.Remove(
            //    fabricantes.Where(c => c.FabricanteId == fabricante.FabricanteId)
            //    .First());

            Fabricante fabricante = context.Fabricantes.Find(id);
            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();

            TempData["Message"] = "Fabricante	" + fabricante.Nome.ToUpper() + "	foi	removido";
            return RedirectToAction("Index");
        }
    }
}