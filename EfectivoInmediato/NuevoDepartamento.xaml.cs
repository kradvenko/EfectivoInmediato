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
    /// Lógica de interacción para NuevoDepartamento.xaml
    /// </summary>
    public partial class NuevoDepartamento : Window
    {
        Intereses iParent;
        public NuevoDepartamento(Window parent)
        {
            InitializeComponent();
            if (parent is Intereses)
            {
                iParent = (Intereses)parent;
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GuardarDepartamento(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tbDepartamento.Text.Length > 0)
                {
                    String resultado = cDepartamento.AgregarDepartamento(tbDepartamento.Text);
                    MessageBox.Show(resultado);
                    if (iParent != null)
                    {
                        iParent.CargarDepartamentos();
                    }
                    this.Close();
                } 
                else
                {
                    MessageBox.Show("No ha ingresado un nombre de departamento.");
                }
            }
        }
    }
}
