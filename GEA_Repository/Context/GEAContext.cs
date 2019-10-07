using GEA_Domain.Entities;
using GEA_Repository.EntityConfig;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace GEA_Repository.Repository.Context
{
    public class GEAContext : DbContext
    {
        public GEAContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Arquivo> Arquivo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // General Custom Context Properties
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ArquivoConfig());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }

    // Classe para trocar a ConnectionString do EF em tempo de execução.
    public static class ChangeDb
    {
        public static void ChangeConnection(this GEAContext context, string cn)
        {
            context.Database.Connection.ConnectionString = cn;
        }
    }
}