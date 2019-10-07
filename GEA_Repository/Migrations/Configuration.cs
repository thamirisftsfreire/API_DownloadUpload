using GEA_Repository.Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEA_Repository.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GEAContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GEAContext context)
        {
        }
    }
}
