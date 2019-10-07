using GEA_Domain.Entities;
using GEA_Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEA_Domain.Specifications.ArquivoSpecification
{
    public class ArquivoTamanhoLimiteSpecification : ISpecification<Arquivo>
    {
        private readonly IArquivoRepository _arquivoRepository;

        public ArquivoTamanhoLimiteSpecification(IArquivoRepository arquivoRepository)
        {
            _arquivoRepository = arquivoRepository;
        }
        public bool IsSatisfiedBy(Arquivo arquivo)
        {
            return arquivo.ImagemArquivo.Length / 1000 <= 10000;
        }
    }
}
