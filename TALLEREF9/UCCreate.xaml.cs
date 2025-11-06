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
    /// Lógica de interacción para UCCreate.xaml
    /// </summary>
    public partial class UCCreate : UserControl
    {

        private  TallerEFContext _context = new TallerEFContext();
        private CollectionViewSource clienteViewSource;
        public UCCreate()
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

        private void GuardarCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente nuevoCliente = new Cliente();
            nuevoCliente.Nombre = ClienteNombreTextBox.Text;
            nuevoCliente.Identificacion = ClienteIdentificacionTextBox.Text;
            _context.Add(nuevoCliente);
            _context.SaveChanges();
            MessageBox.Show("Cliente insertado correctamente", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            ClienteNombreTextBox.Text = "";
            ClienteIdentificacionTextBox.Text = "";
        }
        private void GuardarCuentaCliente_Click(object sender, RoutedEventArgs e)
        {
            CuentaCliente nuevaCuentaCliente = new CuentaCliente();
            nuevaCuentaCliente.Nombre = CuentaClienteNombreTextBox.Text;
            nuevaCuentaCliente.Descripcion = CuentaClienteDescripcionTextBox.Text;
            nuevaCuentaCliente.Saldo = Decimal.Parse(CuentaClienteSaldoTextBox.Text);
            nuevaCuentaCliente.Cliente = (Cliente)CuentaClienteComboBox.SelectedItem;
            _context.Add(nuevaCuentaCliente);
            _context.SaveChanges();
            MessageBox.Show("Cuenta del cliente insertada correctamente", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            CuentaClienteNombreTextBox.Text = "";
            CuentaClienteDescripcionTextBox.Text = "";
            CuentaClienteSaldoTextBox.Text = "";
        }
    }
}
