using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para UCTodoAlternativo.xaml
    /// </summary>
    public partial class UCTodoAlternativo : UserControl
    {
        private TallerEFContext _context = new TallerEFContext();
        private CollectionViewSource clienteViewSource;
        public UCTodoAlternativo()
        {
            InitializeComponent();
            clienteViewSource = (CollectionViewSource)FindResource(nameof(clienteViewSource));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new TallerEFContext();
            _context.Clientes.Load();
            clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
        }

        private void ButtonGuardarCliente_Click(object sender, RoutedEventArgs e)
        {
            int tamanyo = _context.Clientes.Count();
            for (int i=0;i<tamanyo-1;i++)
            {

                try
                {
                    Cliente cliente = (Cliente)clienteDataGrid.Items.GetItemAt(i);
                    Cliente nuevoCliente = (Cliente)cliente;
                    nuevoCliente.Nombre = cliente.Nombre;
                    nuevoCliente.Identificacion = cliente.Identificacion;
                    _context.Update(nuevoCliente);
                    _context.SaveChanges();
  
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al actualizar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            _context = new TallerEFContext();
        }

        private void ButtonDeshacerCliente_Click(object sender, RoutedEventArgs e)
        {
            _context = new TallerEFContext();
        }

 

        private void ButtonGuardarCuenta_Click(object sender, RoutedEventArgs e)
        {
            if (_context.Clientes.Count()>0) {
                int tamanyo = cuentasDataGrid.Items.Count;
                for (int i = 0; i < tamanyo - 1; i++)
                {

                    try
                    {
                        CuentaCliente cc=cuentasDataGrid.Items.GetItemAt(i) as CuentaCliente;
                        CuentaCliente nuevoCuentaCliente = (CuentaCliente)cc;
                        nuevoCuentaCliente.Nombre = cc.Nombre;
                        nuevoCuentaCliente.Descripcion = cc.Descripcion;
                        nuevoCuentaCliente.Saldo = cc.Saldo;
                        nuevoCuentaCliente.Cliente = clienteDataGrid.SelectedItem as Cliente;
                        _context.Update(nuevoCuentaCliente);
                        _context.SaveChanges();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al actualizar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                _context = new TallerEFContext();
            }

        }

        private void ButtonDeshacerCuenta_Click(object sender, RoutedEventArgs e)
        {
            _context = new TallerEFContext();
        }

       
    }
}
