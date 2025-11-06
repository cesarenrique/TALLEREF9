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
    /// Lógica de interacción para UCLookForID.xaml
    /// </summary>
    public partial class UCLookForID : UserControl
    {
        private TallerEFContext _context = new TallerEFContext();
        private CollectionViewSource clienteViewSource;
        public UCLookForID()
        {
            InitializeComponent();
            clienteViewSource = (CollectionViewSource)FindResource(nameof(clienteViewSource));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new TallerEFContext();
            _context.Clientes.Where(p=>p.Id==0).Load();
            clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = TBId.Value.Value;
            _context = new TallerEFContext();
            _context.Clientes.Where(p => p.Id == id).Load();
            clienteViewSource.Source = _context.Clientes.Local.ToObservableCollection();
        }
    }
}
