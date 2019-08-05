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
    /// Lógica de interacción para LiquidacionEspecialRazon.xaml
    /// </summary>
    public partial class LiquidacionEspecialRazon : Window
    {
        Refrendo parent;

        public LiquidacionEspecialRazon(Refrendo p)
        {
            InitializeComponent();
            parent = p;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tbRazon.Text.Length >= 6)
            {
                parent.LiquidacionEspecial(tbRazon.Text);
                this.Close();
            } else
            {
                MessageBox.Show("Escriba al menos 6 caractéres.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbRazon.Focus();
        }
    }
}
