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

        String[] Imagenes = new string[9] { "", "", "", "", "", "", "", "", "" };

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

            for (int i = 0; i < 9; i++)
            {
                if (Imagenes[i] != "")
                {
                    String ruta = @"C:\Efectivo Inmediato\Imagenes Prendas\Prenda_" + idPrenda;

                    System.IO.Directory.CreateDirectory(ruta);

                    String Extension = System.IO.Path.GetExtension(Imagenes[i]);

                    String ArchivoImagen = "Imagen" + (i + 1).ToString() + Extension;

                    string destFile = System.IO.Path.Combine(ruta, ArchivoImagen);

                    System.IO.File.Copy(Imagenes[i], destFile, true);

                    cPrenda.ActualizarImagenPrenda(idPrenda, destFile, (i + 1).ToString());
                }
            }

            this.Close();
        }

        private void AgregarCategoria(object sender, RoutedEventArgs e)
        {
            NuevaCategoriaArticulo categoria = new NuevaCategoriaArticulo(this);
            categoria.ShowDialog();
        }

        private void Porcentaje(object sender, RoutedEventArgs e)
        {
            if (tbAvaluo.Text.Length > 0)
            {
                String por = sender.ToString();

                if (por.Contains("10 %"))
                {
                    CalcularPorcentaje(0.1f);
                }
                else if (por.Contains("20 %"))
                {
                    CalcularPorcentaje(0.2f);
                }
                else if (por.Contains("30 %"))
                {
                    CalcularPorcentaje(0.3f);
                }
                else if (por.Contains("40 %"))
                {
                    CalcularPorcentaje(0.4f);
                }
                else if (por.Contains("50 %"))
                {
                    CalcularPorcentaje(0.5f);
                }
                else if (por.Contains("60 %"))
                {
                    CalcularPorcentaje(0.6f);
                }
                else if (por.Contains("70 %"))
                {
                    CalcularPorcentaje(0.7f);
                }
                else if (por.Contains("80 %"))
                {
                    CalcularPorcentaje(0.8f);
                }
                else if (por.Contains("90 %"))
                {
                    CalcularPorcentaje(0.9f);
                }
            }
        }
        private void CalcularPorcentaje(float p)
        {
            try
            {
                float avaluo = float.Parse(tbAvaluo.Text);
                float por = avaluo * p;
                tbPrestamo.Text = por.ToString();
            }
            catch (Exception e)
            {

            }
        }
        public void GuardarRutaImagen(int Indice, String RutaImagen)
        {
            Imagenes[Indice] = RutaImagen;
        }
        private void MostrarImagen(object sender, RoutedEventArgs e)
        {
            if (tbAvaluo.Text.Length > 0)
            {
                String por = sender.ToString();

                if (por.Contains("1"))
                {
                    if (Imagenes[0] == "")
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "NUEVO", "", 0);
                        VerImagen.ShowDialog();
                    }
                    else
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR NUEVO", Imagenes[0], 0);
                        VerImagen.ShowDialog();
                    }
                }
                else if (por.Contains("2"))
                {
                    if (Imagenes[1] == "")
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "NUEVO", "", 1);
                        VerImagen.ShowDialog();
                    }
                    else
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR NUEVO", Imagenes[1], 1);
                        VerImagen.ShowDialog();
                    }
                }
                else if (por.Contains("3"))
                {
                    if (Imagenes[2] == "")
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "NUEVO", "", 2);
                        VerImagen.ShowDialog();
                    }
                    else
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR NUEVO", Imagenes[2], 2);
                        VerImagen.ShowDialog();
                    }
                }
                else if (por.Contains("4"))
                {
                    if (Imagenes[3] == "")
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "NUEVO", "", 3);
                        VerImagen.ShowDialog();
                    }
                    else
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR NUEVO", Imagenes[3], 3);
                        VerImagen.ShowDialog();
                    }
                }
                else if (por.Contains("5"))
                {
                    if (Imagenes[4] == "")
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "NUEVO", "", 4);
                        VerImagen.ShowDialog();
                    }
                    else
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR NUEVO", Imagenes[4], 4);
                        VerImagen.ShowDialog();
                    }
                }
                else if (por.Contains("6"))
                {
                    if (Imagenes[5] == "")
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "NUEVO", "", 5);
                        VerImagen.ShowDialog();
                    }
                    else
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR NUEVO", Imagenes[5], 5);
                        VerImagen.ShowDialog();
                    }
                }
                else if (por.Contains("7"))
                {
                    if (Imagenes[6] == "")
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "NUEVO", "", 6);
                        VerImagen.ShowDialog();
                    }
                    else
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR NUEVO", Imagenes[6], 6);
                        VerImagen.ShowDialog();
                    }
                }
                else if (por.Contains("8"))
                {
                    if (Imagenes[7] == "")
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "NUEVO", "", 7);
                        VerImagen.ShowDialog();
                    }
                    else
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR NUEVO", Imagenes[7], 7);
                        VerImagen.ShowDialog();
                    }
                }
                else if (por.Contains("9"))
                {
                    if (Imagenes[8] == "")
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "NUEVO", "", 8);
                        VerImagen.ShowDialog();
                    }
                    else
                    {
                        VerImagenPrenda VerImagen = new VerImagenPrenda(this, "MODIFICAR NUEVO", Imagenes[8], 8);
                        VerImagen.ShowDialog();
                    }
                }
            }
        }
    }
}
