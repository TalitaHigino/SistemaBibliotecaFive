using System;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    class Program
    {
        private static readonly string CLIENTECSV = Directory.GetCurrentDirectory() + "\\cliente.csv";
        private static readonly string LIVROCSV = Directory.GetCurrentDirectory() + "\\livro.csv";
        private static readonly string EMPRESTIMOCSV = Directory.GetCurrentDirectory() + "\\emprestimo.csv";
        private const double MULTA = 0.10;

        static void Main(string[] args)
        {
            CriarBancoDeDadosCSV();
            MenuPrincipal();
        }
        #region ClienteService
        private static void RegistrarCliente()
        {
            // registra o cliente e pede informações do cliente           
            Console.WriteLine("\nInforme seu CPF: ");
            string cpf = Console.ReadLine();

            //VALIDAR CPF E VOLTAR PARA O MENU SE JÁ EXISTENTE
            if (ClienteExistente(cpf))
            {
                Console.WriteLine(">>> Cliente já cadastrado <<<");
                MenuPrincipal();
            }

            Console.WriteLine("\nInforme seu Nome Completo: ");
            string nome = Console.ReadLine();

            Console.WriteLine("\nInforme sua data de nascimento: ");
            string dataNasc = Console.ReadLine();

            DateTime dataNascimento;

            //enquanto não cuspir dataNascimento de forma correta, não sairá de dentro do while.
            //exemplo pegado em pesquisa.
            while (!DateTime.TryParseExact(dataNasc, "dd/MM/yyyy", null, DateTimeStyles.None, out dataNascimento))
            {
                Console.WriteLine("Formato inválido");
                Console.WriteLine("\nInforme sua data de nascimento dd/MM/yyyy: ");
                dataNasc = Console.ReadLine();
            }

            Console.WriteLine("\nInforme seu Telefone: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("\nInforme seu Endereço: ");
            Console.WriteLine("\nInforme seu Logradouro: ");
            string logradouro = Console.ReadLine();

            Console.WriteLine("\nInforme seu Bairro: ");
            string bairro = Console.ReadLine();

            Console.WriteLine("\nInforme sua Cidade: ");
            string cidade = Console.ReadLine();

            Console.WriteLine("\nInforme seu Estado: ");
            string estado = Console.ReadLine();

            Console.WriteLine("\nInforme seu CEP: ");
            string cep = Console.ReadLine();

            Console.WriteLine("Cliente cadastrado com sucesso!");
            //instanciando cliente
            Cliente novoCliente = new Cliente()
            {
                Cpf = cpf,
                Nome = nome,
                DataNascimento = dataNascimento,
                Telefone = telefone,

                //instanciando endereço
                Endereco = new Endereco()
                {
                    Logradouro = logradouro,
                    Bairro = bairro,
                    Cidade = cidade,
                    Estado = estado,
                    CEP = cep
                }
            };

            //MUDAR!!!!!
            //novoCliente.IdCliente = 1;

            //new 
            // ClienteService clienteService = new ClienteService();
            novoCliente.IdCliente = BuscarUltimoIdCliente();

            SalvarCliente(novoCliente);
            MenuPrincipal();
        }
        public static long BuscarUltimoIdCliente()
        {
            long ultimoElemento = 1;
            var strLines = File.ReadLines(CLIENTECSV);

            //Pular cabeçalho
            if (strLines.Count() > 1)
            {
                string ultimaLinha = strLines.Last();
                ultimoElemento = long.Parse(ultimaLinha.Split(',')[0]);
            }

            return ultimoElemento + 1;
        }
        private static void SalvarCliente(Cliente cliente)
        {
            File.AppendAllText(CLIENTECSV, cliente.ConverterCSV());
        }
        private static bool ClienteExistente(string cpf)
        {
            bool clienteExiste = false;

            //var: declarar variavel implicamente
            var strLines = File.ReadLines(CLIENTECSV);

            foreach (var line in strLines)
            {
                //ID,NOME,CPF
                //0-ID
                //1-CPF
                //2-NOME
                if (line.Split(',')[1].Trim().Equals(cpf))
                {
                    clienteExiste = true;
                    break;
                }
            }
            return clienteExiste;
        }
        private static long BuscarClienteId(string cpf)
        {
            long id = 0;

            //var: declarar variavel implicamente
            var strLines = File.ReadLines(CLIENTECSV);

            foreach (var line in strLines)
            {
                //ID,NOME,CPF
                //0-ID
                //1-CPF
                //2-NOME
                if (line.Split(',')[1].Trim().Equals(cpf))
                {
                    id = long.Parse(line.Split(',')[0]);
                    break;
                }
            }

            return id;
        }
        #endregion

        #region LivroService
        private static void RegistrarLivro()
        {
            //Cadastra as informaçõs gerais do livro e verifica se existe

            Console.WriteLine("\nInforme o ISBN do livro:  ");
            string isbn = Console.ReadLine();

            if (LivroExistente(isbn))
            {
                Console.WriteLine(">>>Livro já cadastrado <<<");
                MenuPrincipal();
            }

            Console.WriteLine("\nInforme o Título do Livro: ");
            string titulo = Console.ReadLine();

            Console.WriteLine("\nInforme qual é o Gênero: ");
            string genero = Console.ReadLine();

            Console.WriteLine("\nInforme a Data de Publicação: ");
            string datapublicacao = Console.ReadLine();
            DateTime dataPublicacao;
            while (!DateTime.TryParseExact(datapublicacao, "dd/MM/yyyy", null, DateTimeStyles.None, out dataPublicacao))
            {
                Console.WriteLine("Formato inválido");
                Console.WriteLine("\nInforme a Data de Publicação dd/MM/yyyy: ");
                datapublicacao = Console.ReadLine();
            }

            Console.WriteLine("Informe o nome do Autor: ");
            string autor = Console.ReadLine();

            Livro novoLivro = new Livro()
            {
                ISBN = isbn,
                Titulo = titulo,
                Genero = genero,
                DataPublicacao = dataPublicacao,
                Autor = autor
            };

            novoLivro.NumeroTombo = NumeroTombo();

            SalvarLivro(novoLivro);

            //interpolação é bom para não passar indice ex - {0}", numerotombo
            Console.WriteLine($"Livro cadastrado com sucesso!!.Número {novoLivro.NumeroTombo}");

            MenuPrincipal();
        }
        private static void SalvarLivro(Livro livro)
        {
            File.AppendAllText(LIVROCSV, livro.ConverterCSV());
        }
        private static bool LivroExistente(string isbn)
        {
            bool livroExiste = false;

            var lines = File.ReadLines(LIVROCSV);

            foreach (var line in lines)
            {
                if (line.Split(',')[1].Equals(isbn))
                {
                    livroExiste = true;
                    break;
                }
            }
            return livroExiste;
        }
        private static bool LivroDisponivel(long numeroTombo)
        {
            bool livroDisponivel = true;

            var lines = File.ReadLines(EMPRESTIMOCSV);

            foreach (var line in lines)
            {
                if (line.Split(',')[1].Trim().Equals(numeroTombo.ToString()))
                {
                    livroDisponivel = false;
                    break;
                }
            }
            return livroDisponivel;
        }
        private static long NumeroTombo()
        {
            long ultimoElemento = 0;
            var todasAsLinhas = File.ReadLines(LIVROCSV);

            if (todasAsLinhas.Count() > 1)
            {
                string ultimaLinha = todasAsLinhas.Last();
                /*isbn, tombo, nome, genero, data, autor
                 * 0 - isbn
                 * 1 - tombo
                 * 2 - nome
                 * 3 - genero
                 * 4 - data
                 * 5 - autor
                 */
                ultimoElemento = long.Parse(ultimaLinha.Split(',')[0]);
            }

            return ultimoElemento + 1;
        }
        #endregion

        #region EmprestimoService
        private static void EmprestimoLivro()
        {
            Console.WriteLine("Informe o Número do Tombo: ");
            long numeroTombo = long.Parse(Console.ReadLine());
            //VERIFICAR ESTA REGRA, PRA SABER SE REINICIA O PROCESSO DE EMPRESTIMO
            if (!LivroDisponivel(numeroTombo))//consultar tombo 
            {
                Console.WriteLine("Livro indisponível para empréstimo.");
                EmprestimoLivro();
            }

            Console.WriteLine("Informe o seu CPF: ");// consultar cliente
            string cpf = Console.ReadLine();

            if (!ClienteExistente(cpf))
            {
                Console.WriteLine("Cliente não cadastrado.");
                EmprestimoLivro();
            }

            DateTime dataEmprestimo = DateTime.Now;

            Console.WriteLine("Informe a data de Devolução: ");
            string dataDevolucao = Console.ReadLine();

            DateTime datadevolucao;
            while (!DateTime.TryParseExact(dataDevolucao, "dd/MM/yyyy", null, DateTimeStyles.None, out datadevolucao))
            {
                Console.WriteLine("FORMATO INVÁLIDO!");
                Console.WriteLine("Informe a data de Devolução dd/MM/yyyy: ");

                dataDevolucao = Console.ReadLine();
            }

            EmprestimoLivro emprestimo = new EmprestimoLivro
            {
                IdCliente = BuscarClienteId(cpf),
                NumeroTombo = numeroTombo,
                DataEmprestimo = dataEmprestimo,
                DataDevolucao = datadevolucao,
                StatusEmprestimo = 1
            };

            SalvarEmprestimo(emprestimo);
            MenuPrincipal();
        }
        private static void DevolucaoLivro()
        {
            Console.WriteLine("Informe o Número do Tombo: ");
            long numeroTombo = long.Parse(Console.ReadLine());
            //VERIFICAR ESTA REGRA, PRA SABER SE REINICIA O PROCESSO DE DEVOLUÇÃO
            if (!LivroEmprestado(numeroTombo))//consultar tombo 
            {
                Console.WriteLine("Livro não encontrado para devolução");
                DevolucaoLivro();
            }

            DateTime dataDevolucao = BuscarDataDevolucao(numeroTombo);

            int dias = 0;


            if (DateTime.Now > dataDevolucao)
            {
                dias = DateTime.Now.Day - dataDevolucao.Day;
            }

            AtualizarEmprestimo(numeroTombo, 2);

            double multaTotal = 0.0;

            if (dias > 0)
                multaTotal = dias * MULTA;

            Console.WriteLine($"O Valor da Multa total é de {multaTotal}");
        }
        private static DateTime BuscarDataDevolucao(long numeroTombo)
        {
            DateTime dataDevolucao = new DateTime();

            var lines = File.ReadLines(EMPRESTIMOCSV);

            foreach (var line in lines)
            {
                if (line.Split(',')[1].Trim().Equals(numeroTombo.ToString()))
                {
                    dataDevolucao = DateTime.Parse(line.Split(',')[3]);
                    break;
                }
            }

            return dataDevolucao;
        }
        private static bool LivroEmprestado(long numeroTombo)
        {
            bool LivroEmprestado = false;

            var lines = File.ReadLines(EMPRESTIMOCSV);

            foreach (var line in lines)
            {
                if (line.Split(',')[1].Trim().Equals(numeroTombo.ToString()))
                {
                    LivroEmprestado = true;
                    break;
                }
            }
            return LivroEmprestado;
        }
        private static void SalvarEmprestimo(EmprestimoLivro emprestimoLivro)
        {
            File.AppendAllText(EMPRESTIMOCSV, emprestimoLivro.ConverterCSV());
        }
        private static void AtualizarEmprestimo(long numeroTombo, int situacao)
        {
            var lines = File.ReadLines(EMPRESTIMOCSV);

            foreach (var line in lines)
            {
                if (line.Split(',')[1].Trim().Equals(numeroTombo.ToString()))
                {
                    line.Split(',')[4] = situacao.ToString();
                    break;
                }
            }

            //ATUALIZAR LINHA
        }
        #endregion

        #region Principal
        private static void MenuPrincipal()
        {
            //menuprincipal 
            Console.WriteLine(">>> MENU PRINCIPAL <<< ");
            Console.WriteLine("\n1 - Cadastro de cliente." +
                              "\n2 - Cadastro de livro." +
                              "\n3 - Empréstimo de livro." +
                              "\n4 - Devolução de livro." +
                              "\n5 - Relatório de empréstimos e devoluções." +
                              "\n0 - para sair.");
            Console.WriteLine("\nEscolha uma das opções:  ");
            int escolha = int.Parse(Console.ReadLine());

            switch (escolha)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    //cadastrar cliente
                    Console.Clear();
                    RegistrarCliente();

                    break;
                case 2:
                    //cadastrar livro
                    Console.Clear();
                    RegistrarLivro();

                    break;
                case 3:
                    //emprestimo
                    Console.Clear();
                    EmprestimoLivro();

                    break;
                case 4:
                    //devolução
                    Console.Clear();
                    DevolucaoLivro();
                    break;
                case 5:
                    //relatório
                    Console.Clear();
                    break;
                default:
                    break;

            }
        }
        private static void CriarBancoDeDadosCSV()
        {
            string cabecalho;

            //verifico se existe o arquivo
            if (!File.Exists(CLIENTECSV))
            {
                cabecalho = "IdCliente, Cpf, Nome, DataNascimento, Telefone, Logradouro, Bairro, Cidade, Estado, CEP";
                File.WriteAllText(CLIENTECSV, cabecalho);
            }

            if (!File.Exists(LIVROCSV))
            {
                cabecalho = "NumeroTombo, ISBN, Titulo, Genero, DataPublicacao, Autor";
                File.WriteAllText(LIVROCSV, cabecalho);
            }

            if (!File.Exists(EMPRESTIMOCSV))
            {
                cabecalho = "IdCliente, NumeroTombo, DataEmprestimo, DataDevolucao, StatusEmprestimo";
                File.WriteAllText(EMPRESTIMOCSV, cabecalho);
            }

        }
        #endregion
    }
}
