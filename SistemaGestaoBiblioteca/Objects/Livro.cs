namespace SistemaGestaoBiblioteca
{
    public class Livro
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public bool Disponivel { get; set; } = true;
        public string ISBN { get; set; }

        public Livro(int id, string titulo, string autor, string isbn)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
        }

        public override string ToString()
        {
            return $"{Id}: {Titulo} (por {Autor}) - (ISBN: {ISBN}) {(Disponivel ? "Disponível" : "Emprestado")}";
        }
    }
}
