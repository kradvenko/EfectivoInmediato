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
        List<String> periodos;

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
            cbReclamoAnticipadoInteres.SelectedIndex = 0;

            reclamoExtemporaneo = new List<string>();
            reclamoExtemporaneo.Add("Interes");
            reclamoExtemporaneo.Add("Monto Fijo");
            cbReclamoExtemporaneoInteres.SelectedIndex = 0;

            periodos = new List<string>();
            periodos.Add("Mensual");

            cbReclamoAnticipadoInteres.ItemsSource = reclamoAnticipado;
            cbReclamoExtemporaneoInteres.ItemsSource = reclamoExtemporaneo;
            cbPeriodo.ItemsSource = periodos;
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

            cInteres nuevoInteres = new cInteres();

            nuevoInteres.IdDepartamento = cbDepartamento.SelectedValue.ToString();
            nuevoInteres.Periodo = cbPeriodo.Text;
            nuevoInteres.Plazo = tbPlazo.Text;
            nuevoInteres.Financiamiento = tbFinanciamiento.Text;
            nuevoInteres.Almacenaje = tbAlmacenaje.Text;
            nuevoInteres.Administracion = tbAdministracion.Text;
            nuevoInteres.IVA = tbIVA.Text;
            nuevoInteres.PagoMinimo = tbPagoMinimo.Text;
            nuevoInteres.DiasDeGracia = tbDiasGracia.Text;
            nuevoInteres.ReclamoAnticipadoMetodo = cbReclamoAnticipadoInteres.Text;
            nuevoInteres.ReclamoAnticipadoCantidad = tbReclamoAnticipadoCantidad.Text;
            nuevoInteres.ReclamoAnticipadoDias = tbReclamoAnticipadoDias.Text;
            nuevoInteres.ReclamoExtemporaneoMetodo = cbReclamoExtemporaneoInteres.Text;
            nuevoInteres.ReclamoExtemporaneoCantidad = tbReclamoExtemporaneoCantidad.Text;
            nuevoInteres.ReclamoExtemporaneoDias = tbReclamoExtemporaneoDias.Text;

            String res = cInteres.GuardarInteres(cbDepartamento.SelectedValue.ToString(), nuevoInteres);

            int id;
            if (int.TryParse(res, out id))
            {
                MessageBox.Show("Se ha guardado el interes.");
            }
            else
            {
                MessageBox.Show(res);
            }
        }

        private void CbDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cInteres interes = new cInteres();
            interes = cInteres.ObtenerInteresDepartamento(((cDepartamento)cbDepartamento.SelectedItem).IdDepartamento);

            cbPeriodo.Text = interes.Periodo;
            tbPlazo.Text = interes.Plazo;
            tbFinanciamiento.Text = interes.Financiamiento;
            tbAlmacenaje.Text = interes.Almacenaje;
            tbAdministracion.Text = interes.Administracion;
            tbIVA.Text = interes.IVA;
            calcularTotal();
            tbPagoMinimo.Text = interes.PagoMinimo;
            tbDiasGracia.Text = interes.DiasDeGracia;
            cbReclamoAnticipadoInteres.Text = interes.ReclamoAnticipadoMetodo;
            tbReclamoAnticipadoCantidad.Text = interes.ReclamoAnticipadoCantidad;
            tbReclamoAnticipadoDias.Text = interes.ReclamoAnticipadoDias;
            cbReclamoExtemporaneoInteres.Text = interes.ReclamoExtemporaneoMetodo;
            tbReclamoExtemporaneoCantidad.Text = interes.ReclamoExtemporaneoCantidad;
            tbReclamoExtemporaneoDias.Text = interes.ReclamoExtemporaneoDias;
        }

        private void calcularTotal()
        {
            try
            {
                float total = (float.Parse(tbFinanciamiento.Text) + float.Parse(tbAlmacenaje.Text) + float.Parse(tbAdministracion.Text)) * (1 + float.Parse(tbIVA.Text) / 100);
                tbTotal.Text = total.ToString();
                float CAT = total * 12;
                tbCAT.Text = CAT.ToString();
            }
            catch (Exception exc)
            {

            }
        }

        private void ValidaFinanciamiento(object sender, KeyEventArgs e)
        {
            float f;
            if (!float.TryParse(tbFinanciamiento.Text, out f))
            {
                MessageBox.Show("No ha escrito un valor correcto para el financiamiento.");
                tbFinanciamiento.Focus();
                return;
            }
            else
            {
                calcularTotal();
            }
        }

        private void ValidaAlmacenaje(object sender, KeyEventArgs e)
        {
            float f;
            if (!float.TryParse(tbAlmacenaje.Text, out f))
            {
                MessageBox.Show("No ha escrito un valor correcto para el almacenaje.");
                tbAlmacenaje.Focus();
                return;
            }
            else
            {
                calcularTotal();
            }
        }

        private void ValidaAdministracion(object sender, KeyEventArgs e)
        {
            float f;
            if (!float.TryParse(tbAdministracion.Text, out f))
            {
                MessageBox.Show("No ha escrito un valor correcto para la administración.");
                tbAdministracion.Focus();
                return;
            }
            else
            {
                calcularTotal();
            }
        }

        private void ValidaIVA(object sender, KeyEventArgs e)
        {
            float f;
            if (!float.TryParse(tbIVA.Text, out f))
            {
                MessageBox.Show("No ha escrito un valor correcto para el I.V.A.");
                tbIVA.Focus();
                return;
            }
            else
            {
                calcularTotal();
            }
        }
    }
}
