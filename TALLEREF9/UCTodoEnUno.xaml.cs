using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
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
    /// Lógica de interacción para UCTodoEnUno.xaml
    /// </summary>
    public partial class UCTodoEnUno : UserControl
    {
        private TallerEFContext _context = new TallerEFContext();
        private CollectionViewSource clienteViewSource;
        private CollectionViewSource cuentasClienteViewSource;
        private Cliente seleccionado;
        private CuentaCliente cuentaSeleccionada;
        public UCTodoEnUno()
        {
            InitializeComponent();
            clienteViewSource = (CollectionViewSource)FindResource(nameof(clienteViewSource));
            cuentasClienteViewSource = (CollectionViewSource)FindResource(nameof(cuentasClienteViewSource));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new TallerEFContext();
            _context.Clientes.Load();
            clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
            mostrarCuentasTodas();
            limpiarID();
            limpiarCuenta();
            interesClientes();
            seleccionado = null;
        }

        private void ButtonTodos_Click(object sender, RoutedEventArgs e)
        {
            _context = new TallerEFContext();
            _context.Clientes.Load();
            clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
           
            mostrarCuentasTodas();
            limpiarID();
            limpiarCuenta();
            interesClientes();
        }

        private void ButtonID_Click(object sender, RoutedEventArgs e)
        {
            int id = TBId.Value.Value;
            if (id == 0)
            {
                _context = new TallerEFContext();
                _context.Clientes.Where(p => p.Id == id).Load();
                clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
                interesSinClientes();
                mostrarCuentasSinCliente();
            }
            else
            {
                _context = new TallerEFContext();
                _context.Clientes.Where(p => p.Id == id).Load();
                clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
                if (_context.Clientes.Where(p => p.Id == id).Count() > 0) interesCliente(_context.Clientes.Where(p => p.Id == id).First());
                else interesSinClientes();
                mostrarCuentasConId();
            }
        }

        private void GuardarCliente_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            _context = new TallerEFContext();
            if (ClienteIDTextBox.Text == "")
            {
                Cliente nuevoCliente = new Cliente();
                nuevoCliente.Nombre = ClienteNombreTextBox.Text;
                nuevoCliente.Identificacion = ClienteIdentificacionTextBox.Text;
                _context.Add(nuevoCliente);
                _context.SaveChanges();
                MessageBox.Show("Cliente insertado correctamente", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
                ClienteNombreTextBox.Text = "";
                ClienteIdentificacionTextBox.Text = "";
                interesCliente(nuevoCliente);
            }
            else
            {
                try
                {
                    Cliente nuevoCliente = (Cliente)clienteDataGrid.SelectedItem;
                    nuevoCliente.Nombre = ClienteNombreTextBox.Text;
                    nuevoCliente.Identificacion = ClienteIdentificacionTextBox.Text;
                    _context.Update(nuevoCliente);
                    _context.SaveChanges();
                    MessageBox.Show("Cliente actualizado correctamente", "Guardado", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                    id = nuevoCliente.Id;
                    interesCliente(nuevoCliente);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al actualizar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            _context.Clientes.Where(p => p.Id == id).Load();
            clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();

        }

        private void GuardarClienteDeseleccionar_Click(object sender, RoutedEventArgs e)
        {
            limpiarID();
            limpiarCuenta();
            interesSinClientes();
            seleccionado = null;
        }

        private void limpiarID()
        {

            ClienteIDTextBox.Text = "";
            ClienteNombreTextBox.Text = "";
            ClienteIdentificacionTextBox.Text = "";
            BTGuardarCliente.Content = "Guardar";
        }

        private void mostrarCliente(object sender, RoutedEventArgs e)
        {
            limpiarCuenta(); interesSinClientes();
            if (clienteDataGrid.SelectedItem is Cliente seleccionado3)
            {
                _context = new TallerEFContext();
                ClienteIDTextBox.Text = seleccionado3.Id.ToString();
                ClienteNombreTextBox.Text = seleccionado3.Nombre;
                ClienteIdentificacionTextBox.Text = seleccionado3.Identificacion;
                BTGuardarCliente.Content = "Editar";
                    if (_context.Clientes.Where(p => p.Id == seleccionado3.Id).Count()>0) { 
                    seleccionado = _context.Clientes.Where(p => p.Id == seleccionado3.Id).First();
                    mostrarCuentasConId();
                    interesCliente(seleccionado);
                    CuentaClienteCliente.Value = seleccionado.Id;
                }
            }
        }

        private void interesClientes()
        {
            LBIdSelect.Content = "*";
            LBNombreSelect.Content = "todos";
            LBIdentificacionSelect.Content = "los clientes";
        }

        private void interesSinClientes()
        {
            LBIdSelect.Content = "-";
            LBNombreSelect.Content = "ninguno";
            LBIdentificacionSelect.Content = "los clientes";
        }

        private void interesCliente(Cliente? cliente)
        {
            if (cliente is null)
            {
                interesSinClientes();
            }
            else {
                LBIdSelect.Content = cliente.Id;
                LBNombreSelect.Content = cliente.Nombre;
                LBIdentificacionSelect.Content = cliente.Identificacion;

            }
        }

        private void ButtonCuentaRango_Click(object sender, RoutedEventArgs e)
        {
            if (seleccionado!=null) {
                mostrarCuentasConId();
            }
            else
            {
                mostrarCuentasTodas();
                interesClientes();
                seleccionado = null;
            }
        }

        private void mostrarCuentasConId()
        {
       
            if (seleccionado!=null) { 
                int topeMin = TBmin.Value.Value;
                int topeMax = TBmax.Value.Value;
                int id = seleccionado.Id;
                TBId.Value = id;
                var listaCuentaClientes = new List<CuentaCliente>();
                using SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = TallerEF");
                connection.Open();
                String sql = "SELECT cc.Id, cc.Nombre, cc.Descripcion, cc.Saldo, cc.Clienteid FROM CuentasCliente cc,Clientes cl WHERE cl.Id=cc.Clienteid and cl.Id=" + id + " and " + topeMin.ToString() + "<=cc.saldo and cc.saldo<=" + topeMax.ToString();
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                int aux = 0;
                while (reader.Read())
                {
                    CuentaCliente cc = new CuentaCliente();
                    cc.Id = reader.GetInt32(0);
                    cc.Nombre = reader.GetString(1);
                    cc.Descripcion = reader.GetString(2);
                    cc.Saldo = reader.GetDecimal(3);
                    Cliente c = new Cliente();
                    c.Id = reader.GetInt32(4);
                    cc.Cliente = c;
                    listaCuentaClientes.Add(cc);
                    aux++;
                }
                if (aux!=0)
                {
                    interesCuenta(listaCuentaClientes.First());
                }
                cuentasClienteViewSource.Source = new ObservableCollection<CuentaCliente>(listaCuentaClientes);

            }
        }

        private void mostrarCuentasTodas()
        {
            int topeMin = TBmin.Value.Value;
            int topeMax = TBmax.Value.Value;
            var listaCuentaClientes = new List<CuentaCliente>();
            using SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = TallerEF");
            connection.Open();
            String sql = "SELECT * FROM CuentasCliente cc WHERE " + topeMin.ToString() + "<=cc.saldo and cc.saldo<=" + topeMax.ToString();
            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            int aux = 0;
            while (reader.Read())
            {
                CuentaCliente cc = new CuentaCliente();
                cc.Id = reader.GetInt32(0);
                cc.Nombre = reader.GetString(1);
                cc.Descripcion = reader.GetString(2);
                cc.Saldo = reader.GetDecimal(3);
                Cliente c = new Cliente();
                c.Id = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                cc.Cliente = c;
                listaCuentaClientes.Add(cc);
                aux++;
            }
            if (aux != 0)
            {
                interesCuenta(listaCuentaClientes.First());
            }
            cuentasClienteViewSource.Source = new ObservableCollection<CuentaCliente>(listaCuentaClientes);
        }


        private void mostrarCuentasSinCliente()
        {
            int topeMin = TBmin.Value.Value;
            int topeMax = TBmax.Value.Value;
            var listaCuentaClientes = new List<CuentaCliente>();
            using SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = TallerEF");
            connection.Open();
            String sql = "SELECT * FROM CuentasCliente cc WHERE cc.ClienteId like NULL and " + topeMin.ToString() + "<=cc.saldo and cc.saldo<=" + topeMax.ToString();
            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            int aux = 0;
            while (reader.Read())
            {
                CuentaCliente cc = new CuentaCliente();
                cc.Id = reader.GetInt32(0);
                cc.Nombre = reader.GetString(1);
                cc.Descripcion = reader.GetString(2);
                cc.Saldo = reader.GetDecimal(3);
                Cliente c = new Cliente();
                c.Id = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                cc.Cliente = c;
                listaCuentaClientes.Add(cc);
                aux++;
            }
            if (aux != 0)
            {
                interesCuenta(listaCuentaClientes.First());
            }
            cuentasClienteViewSource.Source = new ObservableCollection<CuentaCliente>(listaCuentaClientes);
        }


        private void GuardarCuentaCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente aux = null;
            int id = CuentaClienteCliente.Value.Value;
            if (_context.Clientes.Where(p => p.Id == id).Count() > 0)
            {
                aux=_context.Clientes.Where(p => p.Id == id).First();
                interesCliente(aux);

            }
            if (aux != null)
            {
                if (cuentaSeleccionada == null)
                {
                    CuentaCliente nuevaCuentaCliente = new CuentaCliente();
                    nuevaCuentaCliente.Nombre = CuentaClienteNombreTextBox.Text;
                    nuevaCuentaCliente.Descripcion = CuentaClienteDescripcionTextBox.Text;
                    nuevaCuentaCliente.Saldo = Decimal.Parse(CuentaClienteSaldoTextBox.Text);
                    nuevaCuentaCliente.Cliente = aux;
                    _context.Add(nuevaCuentaCliente);
                    _context.SaveChanges();
                    MessageBox.Show("Cuenta del cliente insertada correctamente", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
                    CuentaClienteNombreTextBox.Text = "";
                    CuentaClienteDescripcionTextBox.Text = "";
                    CuentaClienteSaldoTextBox.Text = "";
                }
                else
                {

                    try
                    {
                        CuentaCliente nuevaCuentaCliente = (CuentaCliente)cuentasDataGrid.SelectedItem;
                        nuevaCuentaCliente.Nombre = CuentaClienteNombreTextBox.Text;
                        nuevaCuentaCliente.Descripcion = CuentaClienteDescripcionTextBox.Text;
                        nuevaCuentaCliente.Saldo = Decimal.Parse(CuentaClienteSaldoTextBox.Text);
                        nuevaCuentaCliente.Cliente = ((Cliente)aux);
                        _context.Update(nuevaCuentaCliente);
                        _context.SaveChanges();
                        MessageBox.Show("Cuenta del cliente actualizado correctamente", "Guardado", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al actualizar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void mostrarCuentaCliente(object sender, RoutedEventArgs e)
        {
            if (cuentasDataGrid.SelectedItem is CuentaCliente seleccionada2)
            {
                _context = new TallerEFContext();
                interesCuenta(seleccionada2);
            }
        }
        private void interesCuenta(CuentaCliente seleccionada2)
        {
            CuentaClienteIdTextBox.Text = seleccionada2.Id.ToString();
            CuentaClienteNombreTextBox.Text = seleccionada2.Nombre;
            CuentaClienteDescripcionTextBox.Text = seleccionada2.Descripcion;
            CuentaClienteSaldoTextBox.Text = seleccionada2.Saldo.ToString();
            CuentaClienteCliente.Value = seleccionada2.Cliente.Id;
            BTGuardarCuenta.Content = "Editar";
            cuentaSeleccionada = seleccionada2;
            if(seleccionado!=null) interesCliente(seleccionado);
        }

        private void limpiarCuenta()
        {
            CuentaClienteIdTextBox.Text = "";
            CuentaClienteNombreTextBox.Text = "";
            CuentaClienteDescripcionTextBox.Text = "";
            CuentaClienteSaldoTextBox.Text = "";    
            BTGuardarCuenta.Content="Guardar";
            cuentaSeleccionada = null;
        }

        private void GuardarCuentaDeseleccionar_Click(object sender, RoutedEventArgs e)
        {
            limpiarCuenta();
            interesSinClientes();
            seleccionado = null;
        }

        private void EliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (seleccionado!=null) {
                try
                {
                    Cliente nuevoCliente = seleccionado;
                    _context.Remove(nuevoCliente);
                    _context.SaveChanges();
                    //cambiarReferenciaClienteAnull(nuevoCliente);
                    MessageBox.Show("Cliente eliminado correctamente", "Guardado", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al eliminar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Error al eliminar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /*
        private void cambiarReferenciaClienteAnull(Cliente nuevoCliente)
        {
            using SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = TallerEF");
            connection.Open();
            String sql = "UPDATE ClienteId=NULL FROM CuentasCliente ClienteID="+nuevoCliente.Id;
            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
        }*/
        private void EliminarCuentaCliente_Click(object sender, RoutedEventArgs e)
        {
            if (cuentaSeleccionada != null)
            {
                try
                {
                    CuentaCliente nuevaCuentaCliente = (CuentaCliente)cuentaSeleccionada;
                    _context.Remove(nuevaCuentaCliente);
                    _context.SaveChanges();
                    MessageBox.Show("Cuenta del cliente eliminada correctamente", "Guardado", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al eliminar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Error al eliminar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
