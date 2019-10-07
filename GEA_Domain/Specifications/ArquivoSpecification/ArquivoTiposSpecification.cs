using GEA_Domain.Entities;
using GEA_Domain.Interfaces.Repository;
using GEA_Domain.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GEA_Domain.Specifications.ArquivoSpecification
{
    public class ArquivoTiposSpecification : ISpecification<Arquivo>
    {
        private readonly IArquivoRepository _arquivoRepository;

        public ArquivoTiposSpecification(IArquivoRepository arquivoRepository)
        {
            _arquivoRepository = arquivoRepository;
        }
        public bool IsSatisfiedBy(Arquivo arquivo)
        {
            var contentType = MimeTypes.GetContentType(arquivo.ImagemArquivo);
            var extension = MimeTypes.GetExtensionFromContentType(contentType);
            return !extension.Contains("bat") && !extension.Contains("exe");
        }
    }
}
