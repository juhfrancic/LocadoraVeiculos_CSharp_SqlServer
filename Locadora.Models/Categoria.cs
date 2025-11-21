using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Categoria
    {
        public readonly static string INSERTCATEGORIA = "EXEC sp_INSERIRCATEGORIA @Nome, @Descricao, @Diaria";

        public readonly static string SELECTNOMECATEGORIAPORID = "SELECT Nome FROM tblCategorias WHERE CategoriaID = @Id";

        public int CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public decimal Diaria { get; private set; }

        public Categoria(string nome, decimal diaria)
        {
            Nome = nome;
            Diaria = diaria;
        }

        public Categoria(string nome, decimal diaria, string? descricao) : this (nome, diaria)
        {
            Descricao = descricao;
        }

        public void setCategoriaId(int categoriaId)
        {
            CategoriaId = categoriaId;
        }

        public override string? ToString()
        {
            return $"Categoria: {Nome}\nDescrição: {Descricao}\nValor da Diária: {Diaria}\n";
        }
    }
}
