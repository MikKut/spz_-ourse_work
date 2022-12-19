using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Protector.Starter;

namespace NineMensMorrisKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GateOpenerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string st = "NineMensMorris";
                string st1 = Directory.GetCurrentDirectory() + "\\" + st + "\\bin\\Debug\\net6.0-windows\\NineMensMorris.exe";
                Starter.Start(ref st);
                this.Close();
                using (var x = Process.Start(st1))
                {
                    x.WaitForExit();
                    // x.Close();
                }

                Starter.End(ref st);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
