﻿using System.Web.Mvc;
using Modelo.Cadastros;
using System.Net;
using Servico.Cadastros;
using Servico.Tabelas;
using System.IO;
using System;

namespace Projeto01.Areas.Cadastros.Controllers
{
    public class ProdutosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        // GET: Produtos
        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
            //return View(context.Produtos.OrderBy(c => c.Nome));
        }

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            return GravarProduto(produto, null);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));
            return ObterVisaoProdutoPorId(id);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(Produto produto, string remover)
        {
            long id = (long)produto.ProdutoId;
            if (remover != null)
            {
                DeletarImagem(id);
            }
            return GravarProduto(produto, remover);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                DeletarImagem(id);
                Produto produto = produtoServico.EliminarProdutoPorId(id);
               
                TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoServico.ObterProdutoPorId((long) id);
            
            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(
                    categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(
                    fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(
                    categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(
                    fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome", produto.FabricanteId);
            }
        }

        private ActionResult GravarProduto(Produto produto, string remover)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produtoServico.GravarProduto(produto);

                    if (remover == null)
                    {
                        UploadImagem(produto);
                    }

                    return RedirectToAction("Index");
                }

                PopularViewBag(produto);
                return View(produto);
            }
            catch
            {
                PopularViewBag(produto);
                return View(produto);
            }
        }

        private void UploadImagem(Produto produto)
        {
            string nomeArquivo = "";
            foreach (var arquivo in produto.Arquivo)
            {
                nomeArquivo = Path.GetFileName(Convert.ToString(produto.ProdutoId) + ".jpg");
                // nomeArquivo = Convert.ToString(produto.ProdutoId);
                var caminho = Path.Combine(Server.MapPath("~/Uploads"), nomeArquivo);
                arquivo.SaveAs(caminho);
            }
        }

        private void DeletarImagem(long id)
        {
            var profileImg = "~/Uploads/" + id.ToString() + ".jpg";
            var absolutePath = Server.MapPath(profileImg);

            System.IO.File.Delete(absolutePath);

        }
    }
}
