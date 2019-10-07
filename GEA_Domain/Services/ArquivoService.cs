using GEA_Domain.Entities;
using GEA_Domain.Interfaces.Repository;
using GEA_Domain.Interfaces.Services;
using GEA_Domain.Specifications.ArquivoSpecification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEA_Domain.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IArquivoRepository _arquivoRepository;
        private Arquivo _arquivo;
        public ArquivoService(IArquivoRepository arquivoRepository, Arquivo arquivo)
        {
            _arquivoRepository = arquivoRepository;
            _arquivo = arquivo;
        }
        public Arquivo Adicionar(Arquivo arquivo)
        {
            if(!new ArquivoTamanhoLimiteSpecification(_arquivoRepository).IsSatisfiedBy(arquivo))
            {
                throw new Exception("Não são permitidos arquivos maiores que 10MB.");
            }
            if (!new ArquivoTiposSpecification(_arquivoRepository).IsSatisfiedBy(arquivo))
            {
                throw new Exception("Não são permitidos arquivos do tipo .BAT e .EXE");
            }
            if(_arquivoRepository.ObterArquivo(arquivo) != null)
            {
                throw new Exception("Já existe um arquivo com este nome.");
            }
            return _arquivoRepository.Adicionar(arquivo);            
        }
        
        public void Dispose()
        {
            _arquivoRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public Arquivo ObterArquivo(Arquivo arquivo)
        {
            var _retornoArquivo = _arquivoRepository.ObterArquivo(arquivo);
            if(_retornoArquivo == null)
                throw new Exception("Arquivo não encontrado.");
            else
                return _retornoArquivo;
        }
        #region File Server
        public void AdicionarArquivoFileServer(Arquivo arquivo)
        {
            if (!new ArquivoTamanhoLimiteSpecification(_arquivoRepository).IsSatisfiedBy(arquivo))
            {
                throw new Exception("Não são permitidos arquivos maiores que 10MB.");
            }
            if (!new ArquivoTiposSpecification(_arquivoRepository).IsSatisfiedBy(arquivo))
            {
                throw new Exception("Não são permitidos arquivos do tipo .BAT e .EXE");
            }
            if (arquivo.ImagemArquivo.Length > 0)
            {
                string[] files = System.IO.Directory.GetFiles(arquivo.CaminhoArquivo);
                if (Directory.Exists(arquivo.CaminhoArquivo))
                {
                    File.Delete(arquivo.CaminhoArquivo + "\\" + arquivo.NomeArquivo);
                }

                Directory.CreateDirectory(arquivo.CaminhoArquivo);
                File.WriteAllBytes(arquivo.CaminhoArquivo + arquivo.NomeArquivo, arquivo.ImagemArquivo);
            }

        }
        public Arquivo ObterArquivoFileServer(Arquivo arquivo)
        {
            if (System.IO.Directory.Exists(arquivo.CaminhoArquivo))
            {
                _arquivo.ImagemArquivo = File.ReadAllBytes(arquivo.CaminhoArquivo + "\\" + arquivo.NomeArquivo);
                return _arquivo;
            }
            else
                return null;
        }
        #endregion
    }
}
