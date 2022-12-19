using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace NineMensMorris.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Rules : Page
    {
        Windows.MainWindow _content;
        public Rules(Windows.MainWindow content)
        {
            InitializeComponent();
            _content = content;
            try
            {
                textBlockContent.Text = GetRulesText();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message + "\nHint: Check \"Rules.txt\" file");
                _content.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _content.mainFrame.GoBack();
            }
            _content=content;
        }
        private string GetRulesText()
        {
            string fullPath = Directory.GetCurrentDirectory().Replace("NineMensMorris\\bin\\Debug\\net6.0-windows", "NineMensMorris\\Rules.txt");
            return File.ReadAllText(fullPath, Encoding.UTF8);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _content.mainFrame.GoBack();
        }
    }
}
