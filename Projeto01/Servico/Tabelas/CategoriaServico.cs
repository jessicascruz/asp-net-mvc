using System.Linq;
using Modelo.Tabelas;
using Modelo.Cadastros;
using Persistencia.DAL.Tabelas;

namespace Servico.Tabelas
{
    public class CategoriaServico
    {
        private CategoriaDAL categoriaDAL = new CategoriaDAL();

        public IQueryable<Categoria> ObterCategoriasClassificadasPorNome()
        {
            return categoriaDAL.ObterCategoriasClassificadasPorNome();
        }

        public Categoria ObterCategoriaPorId(long id)
        {
            return categoriaDAL.ObterCategoriaPorId(id);
        }

        public void GravarCategoria(Categoria categoria)
        {
            categoriaDAL.GravarCategoria(categoria);
        }

        public Categoria EliminarCategoriaPorId(long id)
        {
            return categoriaDAL.EliminarCategoriaPorId(id);
        }

    }
}
