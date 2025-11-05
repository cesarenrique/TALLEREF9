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

namespace TALLEREF9
{
    /// <summary>
    /// Lógica de interacción para UCRead.xaml
    /// </summary>
    public partial class UCRead : UserControl
    {
        private readonly TallerEFContext _context = new TallerEFContext();
        private CollectionViewSource clienteViewSource;
        public UCRead()
        {
            InitializeComponent();
            clienteViewSource = (CollectionViewSource)FindResource(nameof(clienteViewSource));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Clientes.Load();
            clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
        }
    }
}
