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
    /// Lógica de interacción para NuevaCategoriaArticulo.xaml
    /// </summary>
    public partial class NuevaCategoriaArticulo : Window
    {
        NuevoArticulo parent;

        public NuevaCategoriaArticulo(NuevoArticulo p)
        {
            InitializeComponent();
            parent = p;
            tbCategoria.Focus();
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TbCategoria_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tbCategoria.Text.Length > 0)
                {
                    cCategoria c = new cCategoria();
                    c.Categoria = tbCategoria.Text;
                    String r = cCategoria.AgregarCategoriaArticulo(c);
                    int i;
                    if (int.TryParse(r, out i))
                    {
                        c.IdCategoria = r;
                        if (parent != null)
                        {
                            parent.ActualizarCategorias(c);
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error."); ;
                    }
                }
                else
                {
                    MessageBox.Show("No ha ingresado un nombre de departamento.");
                }
            }
        }
    }
}
