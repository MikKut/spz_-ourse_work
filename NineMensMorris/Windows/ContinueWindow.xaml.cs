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
using System.IO;

namespace NineMensMorris.Windows
{
    /// <summary>
    /// Логика взаимодействия для ContinueWindow.xaml
    /// </summary>
    public partial class ContinueWindow : Window
    {
        Windows.MainWindow _content;
        public ContinueWindow(Windows.MainWindow content)
        {
            InitializeComponent();
            _content = content;
            SetSaves();
            _content.IsEnabled = false;
        }
        public void CloseButton(object sender, EventArgs args)
        {
            _content.IsEnabled = true;
            _content.Topmost = true;
        }
        private void SetSaves()
        {
            SetButtonToAPanel("New Game", NewGameButtonClick);
            string[] namesOfTheSavings = FilesOperations.SaveHandler.GetNamesArray();
            foreach (string name in namesOfTheSavings)
            {
                if (name != "")
                {
                    SetButtonToAPanel(name, ClickButton);
                }
            }
        }
        private void SetButtonToAPanel(string name, RoutedEventHandler action)
        {
            var newGameButton = new Button();
            newGameButton.Content = name;
            newGameButton.Click += action;
            SPanel.Children.Add(newGameButton);
        }
        private void NewGameButtonClick(object sender, EventArgs args)
        {
            _content.mainFrame.Content = new Pages.GamePage(_content, false);
            this.Close();
        }
        private void ClickButton(object sender, EventArgs args)
        {
            _content.IsEnabled = true;
            var button = (Button)sender;
            FilesOperations.SaveHandler.Load(button.Content.ToString());
            _content.mainFrame.Content = new Pages.GamePage(_content, true);
            this.Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
