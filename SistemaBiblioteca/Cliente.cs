using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    class Cliente
    {
        public long IdCliente{ get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }


        
        public string ConverterCSV()
        {
            return $"\n{IdCliente};{Cpf};{Nome};{DataNascimento};{Telefone}; {Endereco}";
        }
    }
}
