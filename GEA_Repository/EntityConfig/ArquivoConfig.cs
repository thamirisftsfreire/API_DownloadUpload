using GEA_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEA_Repository.EntityConfig
{
    public class ArquivoConfig : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoConfig()
        {
            HasKey(c => c.IdArquivo);

            Property(c => c.NomeArquivo)
                .IsRequired()
                .HasMaxLength(200);

            Property(c => c.CaminhoArquivo)
                .IsRequired()
                .HasMaxLength(200);

            Property(c => c.ImagemArquivo)
                .IsRequired();


            Ignore(c => c.DataCadastro);

            ToTable("Arquivo");
        }
    }
}
