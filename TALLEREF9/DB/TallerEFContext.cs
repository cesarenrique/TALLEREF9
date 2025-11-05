using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALLEREF9.Modelo;

namespace TALLEREF9.DB
{

    public class TallerEFContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TallerEF");
            optionsBuilder.UseLazyLoadingProxies();
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CuentaCliente> CuentasCliente { get; set; }
    }
}