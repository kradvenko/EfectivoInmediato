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
        ObservableCollection<cCategoria> categorias;

        NuevoPrestamo parent;
        String IdCliente;

        public NuevoArticulo(NuevoPrestamo p, String id)
        {
            InitializeComponent();

            departamentos = cDepartamento.ObtenerDepartamentos();
            cbDepartamento.ItemsSource = departamentos;
            cbDepartamento.DisplayMemberPath = "Departamento";
            cbDepartamento.SelectedValuePath = "IdDepartamento";
            cbDepartamento.SelectedIndex = 0;

            categorias = cCategoria.ObtenerCategorias();
            cbCategorias.ItemsSource = categorias;
            cbCategorias.DisplayMemberPath = "Categoria";
            cbCategorias.SelectedValuePath = "IdCategoria";
            cbCategorias.SelectedIndex = 0;

            parent = p;

            IdCliente = id;
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ActualizarCategorias(cCategoria c)
        {
            categorias = cCategoria.ObtenerCategorias();
            cbCategorias.ItemsSource = null;
            cbCategorias.ItemsSource = categorias;
            cbCategorias.SelectedItem = c;
        }

        private void Guardar(object sender, RoutedEventArgs e)
        {
            if (tbDescripcion.Text.Length == 0)
            {
                MessageBox.Show("No ha ingresado la descripción del artículo.");
                tbDescripcion.Focus();
                return;
            }
            if (cbDepartamento.SelectedIndex < 0)
            {
                MessageBox.Show("No ha elegido un departamento.");
                cbDepartamento.Focus();
                return;
            }
            if (tbAvaluo.Text.Length == 0)
            {
                MessageBox.Show("No ha escrito el avalúo de la prenda.");
                tbAvaluo.Focus();
                return;
            }
            float f;
            if (!float.TryParse(tbAvaluo.Text, out f))
            {
                MessageBox.Show("No ha escrito un avalúo correcto.");
                tbAvaluo.Focus();
                return;
            }
            if (tbPrestamo.Text.Length == 0)
            {
                MessageBox.Show("No ha escrito la cantidad del préstamo.");
                tbPrestamo.Focus();
                return;
            }
            if (!float.TryParse(tbPrestamo.Text, out f))
            {
                MessageBox.Show("No ha escrito una cantidad correcta para el préstamo.");
                tbPrestamo.Focus();
                return;
            }
            if (cbCategorias.SelectedIndex < 0)
            {
                MessageBox.Show("No ha elegido una categoría.");
                cbCategorias.Focus();
                return;
            }
            //Si no hay parent significa que no viene de un préstamo el ingreso de la prenda y se meterá
            //directamente al inventario para su venta.
            String EnVenta = "NO";
            if (parent == null)
            {
                EnVenta = "SI";
            }

            String idPrenda = cPrenda.GuardarPrenda(cbDepartamento.SelectedValue.ToString(), IdCliente, ((cCategoria)cbCategorias.SelectedItem).IdCategoria, "ARTICULO", tbDescripcion.Text, tbMarca.Text, tbModelo.Text, tbSerie.Text, "0", "0", "0", "0", "0", "-", "-", "-", "0", "-", "0", "0", "-", "0", "0", "-", "-", "-", tbUbicacionAlmacen.Text, tbObservaciones.Text, tbAvaluo.Text, tbPrestamo.Text, EnVenta);
            cPrenda p = cPrenda.ObtenerPrendaId(idPrenda);
            if (parent != null)
            {
                parent.AgregarPrenda(p);
            }
            this.Close();
        }

        private void AgregarCategoria(object sender, RoutedEventArgs e)
        {
            NuevaCategoriaArticulo categoria = new NuevaCategoriaArticulo(this);
            categoria.ShowDialog();
        }
    }
}
