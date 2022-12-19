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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NineMensMorris.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExitPage.xaml
    /// </summary>
    public partial class ExitPage : Page
    {
        Windows.MainWindow _content;
        public ExitPage(Windows.MainWindow content)
        {
            InitializeComponent();
            _content = content;
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            _content.Close();
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            _content.mainFrame.GoBack();
        }
    }
}
