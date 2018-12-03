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
    /// Lógica de interacción para Intereses.xaml
    /// </summary>
    public partial class Intereses : Window
    {
        ObservableCollection<cDepartamento> departamentos;
        public Intereses()
        {
            InitializeComponent();
            departamentos = cDepartamento.ObtenerDepartamentos();
            cbDepartamento.ItemsSource = departamentos;
            cbDepartamento.DisplayMemberPath = "Departamento";
            cbDepartamento.SelectedValuePath = "IdDepartamento";
        }

        private void AgregarDepartamento(object sender, RoutedEventArgs e)
        {
            NuevoDepartamento departamento = new NuevoDepartamento(this);
            departamento.ShowDialog();
        }

        public void CargarDepartamentos()
        {
            departamentos = cDepartamento.ObtenerDepartamentos();
            cbDepartamento.ItemsSource = departamentos;
        }
    }
}
