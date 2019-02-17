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
    /// Lógica de interacción para NuevoTipoVehiculo.xaml
    /// </summary>
    public partial class NuevoTipoVehiculo : Window
    {
        NuevoVehiculo iParent;

        public NuevoTipoVehiculo(NuevoVehiculo p)
        {
            InitializeComponent();
            iParent = p;
        }

        private void GuardarTipoVehiculo(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tbTipoVehiculo.Text.Length > 0)
                {
                    String r = cTipoVehiculo.AgregarTipoVehiculo(tbTipoVehiculo.Text);
                    MessageBox.Show(r);
                    if (iParent != null)
                    {
                        iParent.ActualizarTiposVehiculo();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No ha ingresado un nombre de departamento.");
                }
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
