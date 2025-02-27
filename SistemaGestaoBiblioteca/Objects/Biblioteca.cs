using SistemaGestaoBiblioteca.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestaoBiblioteca
{
    public class Biblioteca
    {
        #region variáveis
        public List<Livro> Livros { get; set; } = new List<Livro>();
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        private List<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();
        private List<IObserver> Observers { get; set; } = new List<IObserver>();
        private int ProximoIdLivro = 1;
        private int ProximoIdUsuario = 1;
        private int ProximoIdEmprestimo = 1;
        #endregion

        #region Métodos
        // Métodos para gerenciar observers
        public void Attach(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Observers.Remove(observer);
        }

        private void NotificarObservers(Livro livro)
        {
            foreach (var observer in Observers)
            {
                observer.Update(livro);
            }
        }

        // Cadastrar Livro
        public void CadastrarLivro(string titulo, string autor, string isbn)
        {
            try
            {
                var livro = new Livro(ProximoIdLivro++, titulo, autor, isbn);
                Livros.Add(livro);
                Console.WriteLine($"Livro cadastrado com sucesso: {livro}");
            }
            catch (Exception err)
            {
                Generic.GerarMensagemErro(err.Message);
            }

        }

        // Cadastrar Usuário
        public void CadastrarUsuario(string nome)
        {
            try
            {
                var usuario = new Usuario(ProximoIdUsuario++, nome);
                Usuarios.Add(usuario);
                Attach(usuario); // Registra o usuário como observer
                Console.WriteLine($"Usuário cadastrado com sucesso: {usuario}");
            }
            catch (Exception err)
            {
                Generic.GerarMensagemErro(err.Message);
            }

        }

        // Realizar Empréstimo
        public void RealizarEmprestimo(int idLivro, int idUsuario)
        {
            try
            {
                var livro = Livros.FirstOrDefault(l => l.Id == idLivro);
                var usuario = Usuarios.FirstOrDefault(u => u.Id == idUsuario);

                if (livro == null || usuario == null)
                {
                    Console.WriteLine("Livro ou usuário não encontrado.");
                    return;
                }

                if (!livro.Disponivel)
                {
                    Console.WriteLine("Livro já está emprestado.");
                    return;
                }

                var emprestimo = new Emprestimo(ProximoIdEmprestimo++, livro, usuario);
                livro.Disponivel = false;
                Emprestimos.Add(emprestimo);
                Console.WriteLine($"Empréstimo realizado com sucesso: {emprestimo}");
            }
            catch (Exception err)
            {
                Generic.GerarMensagemErro(err.Message);
            }


        }

        // Devolver Livro
        public void DevolverLivro(int idEmprestimo)
        {
            try
            {
                var emprestimo = Emprestimos.FirstOrDefault(e => e.Id == idEmprestimo);

                if (emprestimo == null)
                {
                    Console.WriteLine("Empréstimo não encontrado ou inexistente.");
                    return;
                }
                else
                {
                    emprestimo.Devolver();
                    Console.WriteLine($"Livro devolvido: {emprestimo}");

                    // Notifica os observers (usuários) que o livro está disponível
                    NotificarObservers(emprestimo.Livro);
                }
            }
            catch (Exception err)
            {
                Generic.GerarMensagemErro(err.Message);
            }

        }

        // Listar Livros Disponíveis
        public void ListarLivrosDisponiveis()
        {
            try
            {
                var livrosDisponiveis = Livros.Where(l => l.Disponivel).ToList();

                if (livrosDisponiveis.Count > 0)
                {
                    Console.WriteLine("Livros disponíveis:");
                    livrosDisponiveis.ForEach(Console.WriteLine);
                }
                else
                    Console.WriteLine("Não existe(m) Livro(s) disponível.");


            }
            catch (Exception err)
            {
                Generic.GerarMensagemErro(err.Message);
            }

        }

        // Listar Usuarios
        public void ListarUsuarios()
        {
            try
            {
                var usuarios = Usuarios.ToList();
                if (usuarios.Count > 0)
                {
                    Console.WriteLine("Usuários cadastrados:");
                    usuarios.ForEach(Console.WriteLine);
                }
                else
                {
                    Console.WriteLine("Não existe(m) Usuário(s) cadastrado(s).");
                }
            }
            catch (Exception err)
            {
                Generic.GerarMensagemErro(err.Message);

            }

        }

        // Listar Livros Emprestados
        public void ListarLivrosEmprestados()
        {

            try
            {
                var livrosEmprestados = Emprestimos.Where(e => !e.Livro.Disponivel).ToList();

                if (livrosEmprestados.Count > 0)
                {
                    Console.WriteLine("Livros emprestados:");
                    livrosEmprestados.ForEach(Console.WriteLine);
                }
                else
                    Console.WriteLine("Não existe(m) Livro(s) emprestado(s).");


            }
            catch (Exception err)
            {
                Generic.GerarMensagemErro(err.Message);
            }
        }

        // Listar Histórico de Empréstimos
        public void ListarHistoricoEmprestimos()
        {
            try
            {
                Console.WriteLine("Histórico de empréstimos:");

                if (Emprestimos.Count > 0)
                    Emprestimos.ForEach(Console.WriteLine);
                else
                    Console.WriteLine("Não existe Histórico de empréstimos:");
            }
            catch (Exception err)
            {
                Generic.GerarMensagemErro(err.Message);
            }

        }

        #endregion

    }
}
