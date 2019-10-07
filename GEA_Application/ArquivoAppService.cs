using GEA_Domain.Entities;
using GEA_Domain.Interfaces.Services;
using GEA_Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEA_Application
{
    public class ArquivoAppService : ApplicationService, IArquivoAppService
    {
        private readonly IArquivoService _arquivoService;

        public ArquivoAppService(IArquivoService arquivoService, IUnitOfWork uow)
            : base(uow)
        {
            _arquivoService = arquivoService;
        }

        public Arquivo Adicionar(Arquivo arquivo)
        {

            BeginTransaction();

            var arquivoReturn = _arquivoService.Adicionar(arquivo);
            

            Commit();

            return arquivoReturn;
        }


        public void Dispose()
        {
            _arquivoService.Dispose();
            GC.SuppressFinalize(this);
        }

        public Arquivo ObterArquivo(Arquivo arquivo)
        {
            return _arquivoService.ObterArquivo(arquivo);
        }
        #region File Server
        public void AdicionarArquivoFileServer(Arquivo arquivo)
        {

            _arquivoService.AdicionarArquivoFileServer(arquivo);

        }
        public Arquivo ObterArquivoFileServer(Arquivo arquivo)
        {
            return _arquivoService.ObterArquivoFileServer(arquivo);
        }
        #endregion
    }
}
