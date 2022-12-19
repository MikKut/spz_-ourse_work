using System;
using System.Windows;
using System.Windows.Input;

namespace NineMensMorris.Windows
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
        private void CtrShortcut1(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
        }
        private void CtrShortcut2(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.ToolWindow;
        }
        private void WindowLoaded(object sender, EventArgs e)
        {
            mainFrame.Content = new Pages.MenuPage(this);
        }
    }
}
