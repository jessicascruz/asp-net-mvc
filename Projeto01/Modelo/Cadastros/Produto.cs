using Modelo.Tabelas;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Cadastros
{
    public class Produto
    {
        [DisplayName("Id")]
        public long? ProdutoId { get; set; }


        [StringLength(100, ErrorMessage = "O nome do produto precisa ter no mínimo 10 caracteres", MinimumLength = 4)]
        [Required(ErrorMessage = "Informe o nome do produto")]
        public string Nome { get; set; }


        [DisplayName("Data de Cadastro")]
        [Required(ErrorMessage = "Informe a data de cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? DataCadastro { get; set; }

        [DisplayName("Categoria")]
        public long? CategoriaId { get; set; }


        [DisplayName("Fabricante")]
        public long? FabricanteId { get; set; }

        public Categoria Categoria { get; set; }
        public Fabricante Fabricante { get; set; }

        public string LogotipoMimeType { get; set; }
        public byte[] Logotipo { get; set; }
        public string NomeArquivo { get; set; }
        public long TamanhoArquivo { get; set; }
    }
}