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
using System.Windows.Shapes;

namespace EfectivoInmediato
{
    /// <summary>
    /// Lógica de interacción para VerificarEliminacion.xaml
    /// </summary>
    public partial class VerificarEliminacion : Window
    {
        cPrestamo prestamo;
        MainWindow parent;

        public VerificarEliminacion(MainWindow p, cPrestamo pre)
        {
            InitializeComponent();
            prestamo = pre;
            parent = p;
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {

        }
    }
}
