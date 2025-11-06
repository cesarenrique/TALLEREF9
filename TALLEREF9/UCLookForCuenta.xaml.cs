using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TALLEREF9.DB;
using TALLEREF9.Modelo;

namespace TALLEREF9
{
    /// <summary>
    /// Lógica de interacción para UCLookForCuenta.xaml
    /// </summary>
    public partial class UCLookForCuenta : UserControl
    {
        private TallerEFContext _context = new TallerEFContext();
        private CollectionViewSource cuentasClienteViewSource3;
        public UCLookForCuenta()
        {
            InitializeComponent();
            cuentasClienteViewSource3 = (CollectionViewSource)FindResource(nameof(cuentasClienteViewSource3));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
            var listaCuentaClientes = new List<CuentaCliente>();
            using SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = TallerEF");
            connection.Open();
            String sql = "SELECT * FROM CuentasCliente";
            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CuentaCliente cc = new CuentaCliente();
                cc.Id = reader.GetInt32(0);
                cc.Nombre = reader.GetString(1);
                cc.Descripcion = reader.GetString(2);
                cc.Saldo = reader.GetDecimal(3);
                Cliente c = new Cliente();
                c.Id= reader.GetInt32(reader.GetOrdinal("ClienteId"));
                cc.Cliente = c;
                listaCuentaClientes.Add(cc);
            }
            //cuentasClienteViewSource3.Source = listaCuentaClientes.ToObservableCollection(); ;
            _context.Clientes.Load();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
   
        }
    }
}
