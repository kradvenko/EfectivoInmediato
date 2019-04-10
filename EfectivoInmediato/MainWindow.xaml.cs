﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Deployment.Application;

namespace EfectivoInmediato
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<cPrestamo> prestamos = new ObservableCollection<cPrestamo>();
        public ObservableCollection<cCliente> clientes = new ObservableCollection<cCliente>();

        public MainWindow()
        {
            InitializeComponent();
            /*cPrestamo c = new cPrestamo();
            c.NombreCliente = "TEST";
            prestamos.Add(c);
            dgPrestamos.ItemsSource = prestamos;
            dgClientes.ItemsSource = prestamos;*/

            prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", "30");
            dgPrestamos.ItemsSource = prestamos;

            clientes = cCliente.ObtenerClientes();
            dgClientes.ItemsSource = clientes;

            string version = null;
            try
            {
                //// get deployment version
                version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (InvalidDeploymentException)
            {
                //// you cannot read publish version when app isn't installed 
                //// (e.g. during debug)
                version = "No instalado.";
            }

            lblVersion.Content = version;
        }

        private void MostrarGrid(String Opcion)
        {
            switch (Opcion)
            {
                case "VerPrestamos":
                    LimpiarGrids();
                    gPrestamos.Visibility = Visibility.Visible;
                    break;
                case "VerClientes":
                    LimpiarGrids();
                    gClientes.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void RecargarClientes()
        {
            clientes = cCliente.ObtenerClientes();
            dgClientes.ItemsSource = null;
            dgClientes.ItemsSource = clientes;
        }
        
        private void LimpiarGrids()
        {
            gPrestamos.Visibility = Visibility.Collapsed;
            gClientes.Visibility = Visibility.Collapsed;
        }

        private void VerPrestamos(object sender, RoutedEventArgs e)
        {
            MostrarGrid("VerPrestamos");
        }

        private void VerClientes(object sender, RoutedEventArgs e)
        {
            MostrarGrid("VerClientes");
        }

        private void VerConfiguracion(object sender, RoutedEventArgs e)
        {
            Configuracion conf = new Configuracion();
            conf.ShowDialog();
        }

        private void NuevoCliente(object sender, RoutedEventArgs e)
        {
            NuevoCliente nuevo = new NuevoCliente(this);
            nuevo.ShowDialog();
        }

        private void NuevoPrestamo(object sender, RoutedEventArgs e)
        {
            NuevoPrestamo prestamo = new NuevoPrestamo(this);
            prestamo.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void RecargarPrestamos()
        {
            prestamos = cPrestamo.ObtenerPrestamos("ACTIVO");
            dgPrestamos.ItemsSource = null;
            dgPrestamos.ItemsSource = prestamos;
        }

        private void Refrendar(object sender, RoutedEventArgs e)
        {
            if (dgPrestamos.SelectedItem != null)
            {
                cPrenda pren = new cPrenda();
                cPrestamo pres = new cPrestamo();
                cCliente cli = new cCliente();

                pres = (cPrestamo)dgPrestamos.SelectedItem;
                pren = cPrenda.ObtenerPrendaIdPrestamo(pres.IdPrestamo);
                cli = cCliente.ObtenerClienteIdPrestamo(pres.IdPrestamo);
                Refrendo refrendo = new Refrendo(pres, pren, cli, this);
                refrendo.ShowDialog();
            }
            
        }

        private void TbDiasVencimiento_KeyUp(object sender, KeyEventArgs e)
        {
            int dias = 0;

            if (int.TryParse(tbDiasVencimiento.Text, out dias))
            {
                prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", tbDiasVencimiento.Text);
                dgPrestamos.ItemsSource = null;
                dgPrestamos.ItemsSource = prestamos;
            }
        }

        private void Finiquitados(object sender, RoutedEventArgs e)
        {
            prestamos = cPrestamo.ObtenerPrestamos("LIQUIDADO");
            dgPrestamos.ItemsSource = null;
            dgPrestamos.ItemsSource = prestamos;
        }

        private void Activos(object sender, RoutedEventArgs e)
        {
            tbDiasVencimiento.Text = "30";
            prestamos = cPrestamo.ObtenerPrestamos("ACTIVO", "30");
            dgPrestamos.ItemsSource = null;
            dgPrestamos.ItemsSource = prestamos;
        }
    }
}
