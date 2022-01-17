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
using System.IO;

namespace Antivirus
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Show_Drives();
        }
        List<string> devlist = new List<string>();
        List<string> devlabel = new List<string>();
        string path1="_";
        string path2 = "WindowsServices";
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Show_Drives();
        }
        private void Show_Drives()
        {
            devlist.Clear();
            comboBox.Items.Clear();
            status.Text = " ";
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    devlist.Add(drive.Name);
                    devlabel.Add(drive.VolumeLabel.ToString());
                    comboBox.Items.Add("[" + drive.Name.Substring(0, 2) + "]" + " "+drive.VolumeLabel.ToString() + "  " + (drive.TotalSize / 1024 / 1024 / 1024) + " GB");
                }
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex != -1)
            {
                button1.IsEnabled = true;
                button.IsEnabled = false;
                comboBox.IsEnabled = false;
                bool isExisDir1 = Directory.Exists(devlist[comboBox.SelectedIndex].ToString() + path1);
                bool isExisDir2 = Directory.Exists(devlist[comboBox.SelectedIndex].ToString() + path2);
                if(isExisDir1 == true || isExisDir2 == true) 
                { 
                    status.Text = "Заражён."; 
                    status.Foreground =Brushes.Red; 
                    button2.IsEnabled = true;
                } 
                else 
                { 
                    status.Text = "Чист."; 
                    status.Foreground = Brushes.Green;
                    button2.IsEnabled = false;
                    
                }
                
            }
            else { MessageBox.Show("Выберите накопитель", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            
        }

        private void Справка_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            button1.IsEnabled = false;
            button.IsEnabled = true;
            comboBox.IsEnabled = true;
            status.Text = " ";
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.Combine(devlist[comboBox.SelectedIndex].ToString(), path1);
            List<string> files = Directory.GetFiles(path, "*.*").ToList();
            List<string> dirs = Directory.GetDirectories(path).ToList();
            foreach(var dir in dirs)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                Directory.Move(dir, devlist[comboBox.SelectedIndex].ToString()+"\\"+dirInfo.Name);
            }
            foreach (var file in files)
            {
                DirectoryInfo filename = new DirectoryInfo(file);
                File.Move(file, devlist[comboBox.SelectedIndex].ToString() + "\\"+ System.IO.Path.GetFileName(file));
            }
            if(Directory.Exists(devlist[comboBox.SelectedIndex].ToString() + path1) == true)
            {
                File.SetAttributes(devlist[comboBox.SelectedIndex].ToString() + path1, FileAttributes.Normal);
                Directory.Delete(devlist[comboBox.SelectedIndex].ToString() + path1, true);
            }

            if (Directory.Exists(devlist[comboBox.SelectedIndex].ToString() + path2) == true)
            {
                File.SetAttributes(devlist[comboBox.SelectedIndex].ToString() + path2, FileAttributes.Normal);
                Directory.Delete(devlist[comboBox.SelectedIndex].ToString() + path2, true); 
            }

            if (Directory.Exists(devlist[comboBox.SelectedIndex].ToString() + path1) == false &&
                Directory.Exists(devlist[comboBox.SelectedIndex].ToString() + path2) == false)
            {
                MessageBox.Show("Успешное удаление", "", MessageBoxButton.OK, MessageBoxImage.Information);
                status.Text = "Чист.";
                status.Foreground = Brushes.Green;
                button2.IsEnabled = false;
                button1.IsEnabled = false;
                button.IsEnabled = true;
                comboBox.IsEnabled = true;
            }
            else { MessageBox.Show("Произошла непредвиденная ошибка", "", MessageBoxButton.OK, MessageBoxImage.Error); }

        }
    }
}