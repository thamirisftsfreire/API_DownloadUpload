using GEA_Domain.Entities;
using GEA_Domain.Interfaces.Repository;
using GEA_Repository.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEA_Repository.Repository
{
    public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
    {

        public ArquivoRepository(GEAContext context)
            : base(context)
        {

        }

        public Arquivo ObterArquivo(Arquivo arquivo)
        {
            return Buscar(c => c.NomeArquivo == arquivo.NomeArquivo).FirstOrDefault();
        }
    }
}
