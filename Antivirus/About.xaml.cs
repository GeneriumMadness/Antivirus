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

namespace Antivirus
{
    /// <summary>
    /// Логика взаимодействия для About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("generiummadness@gmail.com");
            button1.Content = "Скопировано!";
            await Task.Delay(1500);
            button1.Content = "generiummadness@gmail.com";
        }
    }
}
