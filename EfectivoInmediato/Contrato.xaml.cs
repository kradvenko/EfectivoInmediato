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
    /// Lógica de interacción para Contrato.xaml
    /// </summary>
    public partial class Contrato : Window
    {
        public Contrato()
        {
            InitializeComponent();
        }

        private void TbNumeroContrato_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tbNumeroContrato.Text.Length > 0)
                {
                    String r = cContrato.ActualizarNumeroContrato(tbNumeroContrato.Text);

                    if (r == "OK")
                    {
                        MessageBox.Show("Se ha modificado el número de contrato.");
                    }
                    else
                    {
                        MessageBox.Show(r);
                    }
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No ha ingresado un número de contrato.");
                }
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cContrato c = new cContrato();
            c = cContrato.ObtenerContrato();

            tbNumeroContrato.Text = c.NumeroContrato;
        }
    }
}
