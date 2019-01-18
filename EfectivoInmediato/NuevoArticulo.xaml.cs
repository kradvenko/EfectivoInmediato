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
using System.Windows.Shapes;

namespace EfectivoInmediato
{
    /// <summary>
    /// Lógica de interacción para NuevoArticulo.xaml
    /// </summary>
    public partial class NuevoArticulo : Window
    {
        ObservableCollection<cDepartamento> departamentos;

        NuevoPrestamo parent;
        String IdArticuloNuevo;

        public NuevoArticulo()
        {
            InitializeComponent();

            departamentos = cDepartamento.ObtenerDepartamentos();
            cbDepartamento.ItemsSource = departamentos;
            cbDepartamento.DisplayMemberPath = "Departamento";
            cbDepartamento.SelectedValuePath = "IdDepartamento";
            cbDepartamento.SelectedIndex = 0;
        }

        public NuevoArticulo(NuevoPrestamo p)
        {
            InitializeComponent();
            parent = p;
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Guardar(object sender, RoutedEventArgs e)
        {

        }
    }
}
