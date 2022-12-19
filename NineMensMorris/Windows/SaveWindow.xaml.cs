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

namespace NineMensMorris.Windows
{
    /// <summary>
    /// Логика взаимодействия для Save.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        Windows.MainWindow _content;
        public SaveWindow(Windows.MainWindow content)
        {
            InitializeComponent();
            _content = content;
            _content.IsEnabled = false;
        }
        public void CloseButton(object sender, EventArgs args)
        {
            _content.IsEnabled = true;
            _content.Topmost = true;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            FilesOperations.SaveHandler.Save(NameForSaving.Text);
            this.Close();
        }
    }
}
