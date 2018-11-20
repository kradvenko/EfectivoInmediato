using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace EfectivoInmediato
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<cPrestamo> prestamos = new ObservableCollection<cPrestamo>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MostrarGrid(String Opcion)
        {
            switch (Opcion)
            {
                case "VerPrestamos":
                    gPrestamos.Visibility = Visibility.Visible;

                    break;
                
            }
        }

        private void VerPrestamos(object sender, RoutedEventArgs e)
        {
            MostrarGrid("VerPrestamos");
        }
    }
}
