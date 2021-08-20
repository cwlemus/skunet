using Microsoft.EntityFrameworkCore;
using Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class SkuContext : DbContext
    {
        public SkuContext(DbContextOptions<SkuContext> options) : base(options)
        {

        }
        public DbSet<Sku> Skus { get; set; }
        public DbSet<Orden> Ordens { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<LogSku> LogSkus { get; set; }

    }
}
