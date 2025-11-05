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
    /// Lógica de interacción para UCDelete.xaml
    /// </summary>
    public partial class UCDelete : UserControl
    {

        private TallerEFContext _context = new TallerEFContext();
        private CollectionViewSource clienteViewSource;
        private CollectionViewSource cuentasClienteViewSource;
        public UCDelete()
        {
            InitializeComponent();
            clienteViewSource = (CollectionViewSource)FindResource(nameof(clienteViewSource));
            cuentasClienteViewSource = (CollectionViewSource)FindResource(nameof(cuentasClienteViewSource));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new TallerEFContext();
            _context.Clientes.Load();
            _context.CuentasCliente.Load();
            clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
            cuentasClienteViewSource.Source = _context.CuentasCliente.Local.ToObservableCollection();

        }

        private void EliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente nuevoCliente = (Cliente)ClienteComboBox.SelectedItem;
                _context.Remove(nuevoCliente);
                _context.SaveChanges();
                MessageBox.Show("Cliente eliminado correctamente", "Guardado", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EliminarCuentaCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CuentaCliente nuevaCuentaCliente = (CuentaCliente)CuentaClienteComboBox.SelectedItem;
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
    }
}
