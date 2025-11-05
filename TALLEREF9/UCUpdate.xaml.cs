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
    /// Lógica de interacción para UCUpdate.xaml
    /// </summary>
    public partial class UCUpdate : UserControl
    {
        private readonly TallerEFContext _context = new TallerEFContext();
        private CollectionViewSource clienteViewSource;
        private CollectionViewSource clienteViewSource2;
        private CollectionViewSource cuentasClienteViewSource;
        public UCUpdate()
        {
            InitializeComponent();
            clienteViewSource = (CollectionViewSource)FindResource(nameof(clienteViewSource));
            clienteViewSource2 = (CollectionViewSource)FindResource(nameof(clienteViewSource2));
            cuentasClienteViewSource = (CollectionViewSource)FindResource(nameof(cuentasClienteViewSource));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Clientes.Load();
            _context.CuentasCliente.Load();
            clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
            clienteViewSource2.Source = _context.Clientes.Local.ToObservableCollection();
            cuentasClienteViewSource.Source = _context.CuentasCliente.Local.ToObservableCollection();
        }
        private void ActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente nuevoCliente = (Cliente)ClienteComboBox.SelectedItem;
                nuevoCliente.Nombre = ClienteNombreTextBox.Text;
                nuevoCliente.Identificacion = ClienteIdentificacionTextBox.Text;
                _context.Update(nuevoCliente);
                _context.SaveChanges();
                MessageBox.Show("Cliente actualizado correctamente", "Guardado", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al actualizar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ActualizarCuentaCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CuentaCliente nuevaCuentaCliente = (CuentaCliente)CuentaClienteComboBox.SelectedItem;
                nuevaCuentaCliente.Nombre = CuentaClienteNombreTextBox.Text;
                nuevaCuentaCliente.Descripcion = CuentaClienteDescripcionTextBox.Text;
                nuevaCuentaCliente.Saldo = Decimal.Parse(CuentaClienteSaldoTextBox.Text);
                nuevaCuentaCliente.Cliente = ((Cliente)CuentaClienteComboBox2.SelectedItem);
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
