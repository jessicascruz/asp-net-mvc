using System.Collections.Generic;
using System.Web.Mvc;
using System.Net;
using Modelo.Tabelas;
using Servico.Tabelas;

namespace Projeto01.Controllers
{
    public class CategoriasController : Controller
    {
        private CategoriaServico categoriaServico = new CategoriaServico();

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
            return View(categoriaServico.ObterCategoriasClassificadasPorNome());
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
            return GravarCategoria(categoria);
        }

        public ActionResult Edit(long? id)
        {
            //return View(categorias.Where(
            //    m => m.CategoriaId == id).First());
            return ObterVisaoCategoriaPorId(id);
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
            return GravarCategoria(categoria);

        }

        public ActionResult Details(long? id)
        {
            //return View(categorias.Where(
            //    m => m.CategoriaId == id).First());
            return ObterVisaoCategoriaPorId(id);
        }

        public ActionResult Delete(long? id)
        {
            //return View(categorias.Where(
            //    c => c.CategoriaId == id).First());
            return ObterVisaoCategoriaPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            //categorias.Remove(
            //    categorias.Where(c => c.CategoriaId == categoria.CategoriaId)
            //    .First());
            try
            {
                Categoria categoria = categoriaServico.EliminarCategoriaPorId(id);

                TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult ObterVisaoCategoriaPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = categoriaServico.ObterCategoriaPorId((long)id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        private ActionResult GravarCategoria(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoriaServico.GravarCategoria(categoria);
                    return RedirectToAction("Index");
                }
                return View(categoria);
            }
            catch
            {
                return View(categoria);
            }
        }

    }
}