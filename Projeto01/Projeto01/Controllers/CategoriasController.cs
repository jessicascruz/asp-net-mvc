using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Projeto01.Contexts;
using System.Net;
using System.Data.Entity;
using Modelo.Tabelas;

namespace Projeto01.Controllers
{
    public class CategoriasController : Controller
    {
        private EFContext context = new EFContext();

        private static IList<Categoria> categorias = new List<Categoria>()
        {
            new Categoria()
            {
                CategoriaId = 1,
                Nome = "Notebooks"
            },
            new Categoria()
            {
                CategoriaId = 2,
                Nome = "Monitores"
            },
            new Categoria()
            {
                CategoriaId = 3,
                Nome = "Impressoras"
            },
            new Categoria()
            {
                CategoriaId = 4,
                Nome = "Mouses"
            },
            new Categoria()
            {
                CategoriaId = 5,
                Nome = "Desktops"
            },
        };

        // GET: Categorias
        public ActionResult Index()
        {
            // return View(categorias.OrderBy(c => c.Nome));
            return View(context.Categorias.OrderBy(c => c.Nome));
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            //categoria.CategoriaId =
            //    categorias.Select(m => m.CategoriaId).Max() + 1;

            //categorias.Add(categoria);
            context.Categorias.Add(categoria);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            //return View(categorias.Where(
            //    m => m.CategoriaId == id).First());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = context.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            //categorias.Remove(
            //    categorias.Where(m => m.CategoriaId == categoria.CategoriaId).First());
            ////categorias[
            ////    categorias.IndexOf(
            ////        categorias.Where(c => c.CategoriaId == categoria.CategoriaId).First())] = categoria;

            //categorias.Add(categoria);
            if (ModelState.IsValid)
            {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);

        }

        public ActionResult Details(long? id)
        {
            //return View(categorias.Where(
            //    m => m.CategoriaId == id).First());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Find(id);
            
            if(categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        public ActionResult Delete(long? id)
        {
            //return View(categorias.Where(
            //    c => c.CategoriaId == id).First());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Find(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            //categorias.Remove(
            //    categorias.Where(c => c.CategoriaId == categoria.CategoriaId)
            //    .First());
            Categoria categoria = context.Categorias.Find(id);
            context.Categorias.Remove(categoria);
            context.SaveChanges();

            // TempData["Message"] = "Fabricante	" + fabricante.Nome.ToUpper() + "	foi	removido";
            return RedirectToAction("Index");
        }

    }
}