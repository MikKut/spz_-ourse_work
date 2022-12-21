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

        private void ProtectorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pathToDirectory = SelectDirectoryBox.Text;
                if (!Directory.Exists(pathToDirectory))
                {
                    throw new DirectoryNotFoundException($"There is no directory \"{pathToDirectory}\"");
                }

                Starter.CreateEncrypted(ref pathToDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProtectedOpenerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pathToOpenProtected = OpenProtectedBox.Text;
                if (!File.Exists(pathToOpenProtected))
                {
                    throw new DirectoryNotFoundException($"There is no file \"{pathToOpenProtected}\"");
                }

                string pathToExe = Starter.Start(ref pathToOpenProtected);
                this.Close();
                using (var x = Process.Start(pathToExe))
                {
                    x.WaitForExit();
                }

                Starter.End(ref pathToOpenProtected);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
