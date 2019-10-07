using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEA_Domain.Entities
{
    public class Arquivo
    {
        public Int32 IdArquivo { get; set; }
        public string NomeArquivo { get; set; }
        public string CaminhoArquivo { get; set; }
        public Byte[] ImagemArquivo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
