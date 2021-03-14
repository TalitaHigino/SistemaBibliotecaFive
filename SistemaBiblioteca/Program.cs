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
        static void Main(string[] args)
        {
            MenuPrincipal();
        }
        private static void RegistrarCliente()
        {
            // registra o cliente e pede informações do cliente           

            Console.WriteLine("\nInforme seu CPF: ");
            string cpf = Console.ReadLine();

            //VALIDAR CPF E VOLTAR PARA O MENU SE JÁ EXISTENTE
            if (ClienteExistente(cpf))
            {
                Console.WriteLine(">>> Cliente já cadastrado <<<");

            }
            Console.WriteLine("\nInforme seu Nome Completo: ");
            string nome = Console.ReadLine();

            Console.WriteLine("\nInforme sua data de nascimento: ");
            string dataNasc = Console.ReadLine();

            DateTime dataNascimento;

            while (!DateTime.TryParseExact(dataNasc, "dd/MM/yyyy", null, DateTimeStyles.None, out dataNascimento))
            {
                Console.WriteLine("Formato inválido");
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

            Cliente novoCliente = new Cliente()
            {
                Cpf = cpf,
                Nome = nome,
                DataNascimento = dataNascimento,
                Telefone = telefone,

                Endereco = new Endereco()
                {
                    Logradouro = logradouro,
                    Bairro = bairro,
                    Cidade = cidade,
                    Estado = estado,
                    CEP = cep
                }
            };

            novoCliente.IdCliente = BuscarUltimoId();
            SalvarCliente(novoCliente);
        }
        private static long BuscarUltimoId()
        {
            var strLines = File.ReadLines(@"C:\Arquivos");// arrumar

            string ultimaLinha = strLines.Last();

            long ultimoElemento = long.Parse(ultimaLinha.Split(',')[3]);

            return ultimoElemento++;
        }

        private static void SalvarCliente(Cliente cliente)
        {
            using (StreamWriter filaWriter = new StreamWriter(@"C:\Arquivos"))

                filaWriter.WriteLine(cliente.ConverterCSV());

        }

        private static bool ClienteExistente(string cpf)
        {
            bool clienteExiste = false;

            var strLines = File.ReadLines(@"C:\cliente.csv"); // arrumar 

            foreach (var line in strLines)
            {
                //ID,NOME,CPF
                //0-ID
                //1-NOME
                //2-CPF
                if (line.Split(',')[0].Equals(cpf))
                {
                    clienteExiste = true;
                    break;
                }
            }
            return clienteExiste;
        }

        private static void CadastrarLivro()
        {
            //Cadastra as informaçõs gerais do livro e verifica se existe

            Console.WriteLine("\nInforme o ISBN do livro:  ");
            string isbn = Console.ReadLine();
            Console.WriteLine(">>> Procurando ....\n\n\n");
            if (LivroExistente(isbn))
            {
                Console.WriteLine(">>>Livro já cadastrado <<<");
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
        }
        private static void SalvarLivro(Livro livro)
        {
            using (StreamWriter filaWriter = new StreamWriter(@"C:Users\talit\source\repos\SistemaBiblioteca\SistemaBiblioteca"))//arrumar

                filaWriter.WriteLine(livro.ConverterCSVLivro());
        }
        private static bool LivroExistente(string isbn)
        {
            bool livroExiste = false;

            var lines = File.ReadLines(@"C:\Users\talit\source\repos\SistemaBiblioteca\SistemaBiblioteca");//arrumar

            foreach (var line in lines)
            {
                if (line.Split(',')[0].Equals(isbn))
                {
                    livroExiste = true;
                    break;
                }
            }
            return livroExiste;
        }
        private static long NumeroTombo()
        {
            var linha = File.ReadLines(@"C: \Users\talit\source\repos\SistemaBiblioteca\SistemaBiblioteca");// arrumar

            string ultimaLinha = linha.Last();

            long ultimoElemento = long.Parse(ultimaLinha.Split(',')[5]);

            return ultimoElemento++;
        }

        private static void EmprestimosLivro()// falta ainda 
        {
            Console.WriteLine("Informe o Número do Tombo: ");
            long numeroTombo = long.Parse(Console.ReadLine());
            if (numeroTombo != NumeroTombo())//consultar tombo 
            {
                Console.WriteLine("LIVRO NÃO EXISTE!!!");
                CadastrarLivro();
            }
            Console.WriteLine("Informe o seu CPF: ");// consultar cliente
            string cpf = Console.ReadLine();
            if (ClienteExistente(cpf))
            {
                Console.WriteLine("CADASTRO EXISTENTE!!!.");
            }
            else
            {
                Console.WriteLine("CLIENTE NÃO CADATRADO!!!");
                RegistrarCliente();
            }

            Console.WriteLine("A data do Emprestimo: ");
            DateTime dataEmprestimo = DateTime.Now;
            Console.WriteLine("Informe a data de Devolução: ");
            string dataDevolucao = Console.ReadLine();
            DateTime datadevolucao;
            while (!DateTime.TryParseExact(dataDevolucao, "dd/MM/yyyy", null, DateTimeStyles.None, out datadevolucao))
            {
                Console.WriteLine("FORMATO INVÁLIDO!");
                dataDevolucao = Console.ReadLine();
            }


            EmprestimoLivro emprestimo = new EmprestimoLivro
            {
                IdCliente= BuscarUltimoId(), 
                NumeroTombo = numeroTombo,
                DataEmprestimo = dataEmprestimo,
                DataDevolucao = datadevolucao,
                //StatusEmprestimo = statusEmprestimo
            };
        }

        private static void DevolucaoLivro()
        { 
        
        
        
        
        }
        static void MenuPrincipal()
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
                case 1:
                    //cadastrar cliente
                    Console.Clear();
                    RegistrarCliente();

                    break;
                case 2:
                    //cadastrar livro
                    Console.Clear();
                    CadastrarLivro();

                    break;
                case 3:
                    //emprestimo
                    Console.Clear();
                    EmprestimosLivro();

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
    }
}
/*IdCliente – Long - Já Existente .CSV CLIENTE
NumeroTombo – Long – Já Existente .CSV LIVRO
DataEmprestimo – DateTime - Sistêmico
DataDevolucao – DateTime - Usuário
StatusEmprestimo – (1-emprestado, 2-devolvido) – Int – Sistêmico 

 O sistema deve receber o NumeroTombo do exemplar e validar se existe o exemplar disponível, caso o exemplar 
não esteja disponível, uma mensagem deve ser exibida, “Livro indisponível para empréstimo”. 
Após a validação do numeroTombo o usuário deve entrar com o CPF e uma validação deve ser feita para validar 
se o cliente esta cadastrado, caso o cliente não esteja cadastro uma msg deve ser exibida, “Cliente não cadastrado” 
e o processo de empréstimo deve ser reiniciado. Em caso de sucesso das validações o usuário deve entrar com os dados 
restantes para completar o empréstimo. Vale lembrar que sempre deve ser inserido o status do empréstimo com o valor 
de 1(Emprestado) e que a DataEmprestimo deve ser preenchida pelo sistema com a data atual(datetime now). 
Os dados de empréstimo devem ser inseridos no .CSV EMPRESTIMO.

Eu como solicitante, desejo que o sistema tenha uma interface para realizar a devolução de livros.
A devolução deve acontecer da seguinte forma; O usuário deve entrar com o Numero do Tombo e devemos 
validar se o exemplar com aquele numeroTombo está emprestado, caso não esteja uma msg deve ser exibida, “Livro não encontrado para devolução”.
O sistema também deve calcular a multa do empréstimo caso a data atual(datetime now) seja maior que a 
DataDevolucao(que foi inserida no ato do emprestimo), e o valor deve ser especificado em uma constate que terá
o valor de 10 centavos por dia. A multa deve ser exibida em uma msg após a devolução.
A devolução dos livros deve modificar o status do empréstimo no .CSV EMPRESTIMO para 2(Devolvido). 

 
 
 */