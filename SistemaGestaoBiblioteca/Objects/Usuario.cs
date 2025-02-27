using System;
using System.Collections.Generic;

namespace SistemaGestaoBiblioteca
{
    // Classe Usuario (Observer)
    public class Usuario : IObserver
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        private List<Livro> LivrosDesejados { get; set; } = new List<Livro>();

        public Usuario(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        // Método Update do Observer
        public void Update(Livro livro)
        {
            if (LivrosDesejados.Contains(livro))
            {
                Console.WriteLine($"[Atenção Usuário:] {Id} - {Nome}, o livro '{livro.Titulo}' está disponível para locação!");
            }
        }

        // Adicionar livro desejado
        public void AdicionarLivroDesejado(Livro livro)
        {
            LivrosDesejados.Add(livro);
        }

        public override string ToString()
        {
            return $"{Id}: {Nome}";
        }
    }

}
