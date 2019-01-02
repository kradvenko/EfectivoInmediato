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
        List<String> reclamoAnticipado;
        List<String> reclamoExtemporaneo;

        public Intereses()
        {
            InitializeComponent();
            departamentos = cDepartamento.ObtenerDepartamentos();
            cbDepartamento.ItemsSource = departamentos;
            cbDepartamento.DisplayMemberPath = "Departamento";
            cbDepartamento.SelectedValuePath = "IdDepartamento";

            reclamoAnticipado = new List<string>();
            reclamoAnticipado.Add("Interes");
            reclamoAnticipado.Add("Monto Fijo");

            reclamoExtemporaneo = new List<string>();
            reclamoExtemporaneo.Add("Interes");
            reclamoExtemporaneo.Add("Monto Fjo");

            cbReclamoAnticipadoInteres.ItemsSource = reclamoAnticipado;
            cbReclamoExtemporaneoInteres.ItemsSource = reclamoExtemporaneo;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cbDepartamento.SelectedValue == null)
            {
                MessageBox.Show("Elija un departamento.");
                cbDepartamento.Focus();
            }
        }

        private void CbDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cInteres interes = new cInteres();
            interes = cInteres.ObtenerInteresDepartamento(((cDepartamento)cbDepartamento.SelectedItem).IdDepartamento);

            tbPlazo.Text = interes.Plazo;
        }
    }
}
