using System;
using System.Linq;

namespace SistemaGestaoBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var biblioteca = new Biblioteca();
            int menuDeOpcao = 0;

            try
            {
                do
                {
                    Console.WriteLine("\n******* Sistema Gestão de Biblioteca Seidor Versão 1.0.0.1 *******");
                    Console.WriteLine("\n");
                    Console.WriteLine("1. Cadastrar Livro");
                    Console.WriteLine("2. Cadastrar Usuário");
                    Console.WriteLine("3. Realizar Empréstimo");
                    Console.WriteLine("4. Devolver Livro");
                    Console.WriteLine("5. Listar Usuários");
                    Console.WriteLine("6. Listar Livros Disponíveis");
                    Console.WriteLine("7. Listar Livros Emprestados");
                    Console.WriteLine("8. Listar Histórico de Empréstimos");
                    Console.WriteLine("9. Adicionar Livro Desejado para Usuário");
                    Console.WriteLine("10.Sair do Sistema");
                    Console.WriteLine("\n");
                    Console.WriteLine("\n******************************************************************");
                    Console.WriteLine("\n");
                    Console.Write("Escolha uma opção: ");
                    int.TryParse(Console.ReadLine(), out int opcao);
                    menuDeOpcao = opcao;

                    switch (menuDeOpcao)
                    {
                        case 1:
                            Console.Write("Título do livro: ");
                            string titulo = Console.ReadLine();

                            if (string.IsNullOrEmpty(titulo))
                            {
                                Console.Write("Erro: Título do livro inválido");
                                Console.WriteLine("\n");
                                break;
                            }

                            Console.Write("Autor do livro: ");
                            string autor = Console.ReadLine();

                            if (string.IsNullOrEmpty(autor))
                            {
                                Console.Write("Erro: Autor do livro inválido");
                                Console.WriteLine("\n");
                                break;
                            }

                            Console.Write("ISBN do livro: ");
                            string isbn = Console.ReadLine();

                            if (string.IsNullOrEmpty(isbn))
                            {
                                Console.Write("Erro: ISBN do livro inválido");
                                Console.WriteLine("\n");
                                break;
                            }

                            biblioteca.CadastrarLivro(titulo, autor, isbn);
                            break;

                        case 2:
                            Console.Write("Nome do usuário: ");
                            string nome = Console.ReadLine();
                            if (string.IsNullOrEmpty(nome))
                            {
                                Console.Write("Erro: Nome do Usuário inválido");
                                Console.WriteLine("\n");
                            }
                            else
                                biblioteca.CadastrarUsuario(nome);

                            break;

                        case 3:
                            Console.Write("ID do livro: ");

                            int.TryParse(Console.ReadLine(), out int idLivro);

                            if (idLivro == 0)
                            {
                                Console.Write("Erro: ID do livro inválido");
                                Console.WriteLine("\n");
                                break;
                            }

                            Console.Write("ID do usuário: ");
                            int.TryParse(Console.ReadLine(), out int idUsuario);

                            if (idUsuario == 0)
                            {
                                Console.Write("Erro: ID do usuário inválido");
                                Console.WriteLine("\n");
                                break;
                            }

                            biblioteca.RealizarEmprestimo(idLivro, idUsuario);
                            break;

                        case 4:
                            Console.Write("ID do empréstimo: ");
                            int.TryParse(Console.ReadLine(), out int idEmprestimo);

                            if (idEmprestimo == 0)
                            {
                                Console.Write("Erro: ID do empréstimo inválido");
                                Console.WriteLine("\n");
                                break;
                            }

                            biblioteca.DevolverLivro(idEmprestimo);
                            break;

                        case 5:
                            biblioteca.ListarUsuarios();
                            break;

                        case 6:
                            biblioteca.ListarLivrosDisponiveis();
                            break;

                        case 7:
                            biblioteca.ListarLivrosEmprestados();
                            break;

                        case 8:
                            biblioteca.ListarHistoricoEmprestimos();
                            break;

                        case 9:
                            Console.Write("ID do usuário: ");
                            int.TryParse(Console.ReadLine(), out int userId);

                            if (userId == 0)
                            {
                                Console.Write("Erro: ID do usuário inválido");
                                Console.WriteLine("\n");
                                break;
                            }


                            Console.Write("ID do livro desejado: ");
                            int.TryParse(Console.ReadLine(), out int bookId);

                            if (bookId == 0)
                            {
                                Console.Write("Erro: ID do livro inválido");
                                Console.WriteLine("\n");
                                break;
                            }

                            var usuario = biblioteca.Usuarios.FirstOrDefault(u => u.Id == userId);

                            var livro = biblioteca.Livros.FirstOrDefault(l => l.Id == bookId);

                            if (usuario != null && livro != null)
                            {
                                usuario.AdicionarLivroDesejado(livro);
                                Console.WriteLine($"Livro '{livro.Titulo}' adicionado à lista de desejos de {usuario.Nome}.");
                            }
                            else
                                Console.WriteLine("Erro: Usuário ou livro não encontrado.");

                            break;

                        case 10:
                            break;


                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                while (menuDeOpcao != 10);

            }
            catch (Exception err)
            {
                Console.WriteLine($"Erro: {err.Message}");
            }

        }
    }
}
