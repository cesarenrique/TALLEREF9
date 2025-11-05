using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALLEREF9.Modelo
{
    public class CuentaCliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Saldo { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
