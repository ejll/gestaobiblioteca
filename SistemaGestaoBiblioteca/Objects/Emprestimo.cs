using System;

namespace SistemaGestaoBiblioteca
{
    public class Emprestimo
    {

        public int Id { get; set; }
        public Livro Livro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }

        public Emprestimo(int id, Livro livro, Usuario usuario)
        {
            Id = id;
            Livro = livro;
            Usuario = usuario;
            DataEmprestimo = DateTime.Now;
        }

        public void Devolver()
        {
            DataDevolucao = DateTime.Now;
            Livro.Disponivel = true;
        }

        public override string ToString()
        {
            return $"{Id}: {Livro.Titulo} emprestado para {Usuario.Nome} em {DataEmprestimo} {(DataDevolucao.HasValue ? $"- Devolvido em {DataDevolucao}" : "")}";
        }
    }
}
