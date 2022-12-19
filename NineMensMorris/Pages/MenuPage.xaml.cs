using System.Windows;
using System.Windows.Controls;


namespace NineMensMorris.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        Windows.MainWindow _content;
        public MenuPage(Windows.MainWindow content)
        {
            InitializeComponent();
            _content = content;
        }
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            _content.mainFrame.Content = new Pages.GamePage(_content, false);
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            Windows.ContinueWindow cw = new(_content); 
            cw.Topmost = true;
            cw.Show();
        }

        private void Rules_Click(object sender, RoutedEventArgs e)
        {
            _content.mainFrame.Content = new Pages.Rules(_content);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _content.mainFrame.Content = new NineMensMorris.Pages.ExitPage(_content);
        }
    }
}
