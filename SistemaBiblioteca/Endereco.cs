using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    class Endereco
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        public override string ToString()
        {
            return $"{Logradouro}; {Bairro}; {Cidade}; {Estado}; {Estado}; {CEP}";
        }
    }
}

