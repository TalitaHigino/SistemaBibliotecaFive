using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca
{
    class Livro
    {
        public long NumeroTombo { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Genero{ get; set; }
        public DateTime DataPublicacao{ get; set; }
        public string Autor { get; set; }

      
        public string ConverterCSV()
        {
            return $"\n{NumeroTombo};{ISBN};{Titulo};{Genero}; {DataPublicacao}; {Autor}";
        }
    }
}
