using GEA_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEA_Domain.Interfaces.Repository
{
    public interface IArquivoRepository : IRepository<Arquivo>
    {
        Arquivo ObterArquivo(Arquivo arquivo);
    }     
}
