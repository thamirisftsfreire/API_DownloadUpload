using GEA_Domain.Interfaces.Repository;
using GEA_Repository.Repository.Context;
using GEA_Repository.Interfaces;
using GEA_Repository.Repository;
using GEA_Repository.UoW;
using SimpleInjector;
using Container = SimpleInjector.Container;
using GEA_Domain.Interfaces.Services;
using GEA_Domain.Services;
using GEA_Domain.Entities;
using GEA_Application;

namespace GEA_CrossCutting_Ioc
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            //Application
            container.Register<IArquivoAppService, ArquivoAppService>(Lifestyle.Scoped);
            // Domain
            container.Register<IArquivoService, ArquivoService>(Lifestyle.Scoped);
            container.Register<Arquivo>(Lifestyle.Scoped);

            // Infra Dados
            container.Register<IArquivoRepository, ArquivoRepository>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<GEAContext>(Lifestyle.Scoped);
        }
    }
}
