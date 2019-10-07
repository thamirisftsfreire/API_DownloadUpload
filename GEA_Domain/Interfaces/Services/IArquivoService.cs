﻿using GEA_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEA_Domain.Interfaces.Services
{
    public interface IArquivoService : IDisposable
    {
        Arquivo Adicionar(Arquivo arquivo);
        void AdicionarArquivoFileServer(Arquivo arquivo);
        Arquivo ObterArquivo(Arquivo arquivo);
        Arquivo ObterArquivoFileServer(Arquivo arquivo);
    }
}