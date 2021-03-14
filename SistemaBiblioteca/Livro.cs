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

        public override string ToString()
        {
            return ">>> DADOS DO LIVRO <<<\n Número do Tombo : " + NumeroTombo + "ISBN: " +ISBN + "Título:" + Titulo + "Gênero: " + Genero +
                "Data de Publicação: " + DataPublicacao + "Autor: "+ Autor +ToString();
        }
        public string ConverterCSVLivro()
        {
            return $"{NumeroTombo},{ISBN},{Titulo},{Genero}, {DataPublicacao}, {Autor}";
        }
    }
}
