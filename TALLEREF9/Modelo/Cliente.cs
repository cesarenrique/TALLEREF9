using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALLEREF9.Modelo
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }

        public virtual ICollection<CuentaCliente> Cuentas { get; set; } = new ObservableCollection<CuentaCliente>();
    }
}