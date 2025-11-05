using Microsoft.EntityFrameworkCore;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Se crea el contexto
        private readonly TallerEFContext _context = new TallerEFContext();
        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            //Se crea la BD. Eliminar esto en produccion
            _context.Database.EnsureCreated();
            //Se cargan los clientes
            _context.Clientes.Load();
            //Se cargan el numero de clientes en la etiqueta
            NClientesLabel.Content = _context.Clientes.Count();
        }
        
    }
}