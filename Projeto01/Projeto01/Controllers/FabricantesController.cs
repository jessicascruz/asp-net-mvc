using Modelo.Cadastros;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Servico.Cadastros;

namespace Projeto01.Controllers
{
    public class FabricantesController : Controller
    {
        private FabricanteServico fabricanteServico = new FabricanteServico();

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
            return View(fabricanteServico.ObterFabricantesClassificadosPorNome());
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

            return GravarFabricante(fabricante);
        }

        public ActionResult Edit(long? id)
        {
            //return View(fabricantes.Where(
            //    m => m.FabricanteId == id).First());
            return ObterVisaoFabricantePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            //fabricantes.Remove(
            //    fabricantes.Where(m => m.FabricanteId == fabricante.FabricanteId).First());

            //fabricantes.Add(fabricante);

            //return RedirectToAction("Index");
            return GravarFabricante(fabricante);
        }

        public ActionResult Details(long? id)
        {
            // return View(fabricantes.Where(
            //    m => m.FabricanteId == id).First());
            return ObterVisaoFabricantePorId(id);
        }

        public ActionResult Delete(long? id)
        {
            //return View(fabricantes.Where(
            //    c => c.FabricanteId == id).First());
            return ObterVisaoFabricantePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            //fabricantes.Remove(
            //    fabricantes.Where(c => c.FabricanteId == fabricante.FabricanteId)
            //    .First());
            try
            {
                Fabricante fabricante = fabricanteServico.EliminarFabricantePorId(id);

                TempData["Message"] = "Fabricante " + fabricante.Nome.ToUpper() + " foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        private ActionResult ObterVisaoFabricantePorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = fabricanteServico.ObterFabricantePorId((long)id);

            if (fabricante == null)
            {
                return HttpNotFound();
            }

            return View(fabricante);
        }

        private ActionResult GravarFabricante(Fabricante fabricante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fabricanteServico.GravarFabricante(fabricante);
                    return RedirectToAction("Index");
                }
                return View(fabricante);
            }
            catch
            {
                return View(fabricante);
            }
        }
    }
}