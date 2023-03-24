using Fight_Club;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Serialization;

namespace Fight_Club
{
    public partial class Window1 : Window
    {
        private bool isMaximized = false;
        public Window1()
        {
            InitializeComponent();
            myListView.ItemsSource = LoadToGrid("characters.txt");
            nameShowLabel.Content = MainWindow.nowName;

            buttonMainMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            ExitButton.Content = "\u2A09";
            EnlargeButton.Content = "\u25FB";
            RollUpButton.Content = "\u2500";

            setTopPanelImage();
        }

        void setTopPanelImage()
        {
            var json = GeneralOptions.GetUsers();

            foreach(var item in json)
            {
                if (MainWindow.nowName == item.Nickname)
                {
                    string url = string.Empty;
                    if(string.IsNullOrEmpty(item.URL))
                        url = defaultImage;
                    else
                        url = item.URL;

                    var bitmap = new BitmapImage(new Uri(url));
                    topPanelImage.Fill = new ImageBrush(bitmap);
                }
            }
        }

        private List<User> LoadToGrid(string path)
        {
            var list = GeneralOptions.GetUsers();
            foreach(var item in list)
            { 
                int length = item.Password.Length;
                item.Password = "";
                for(int i = 0; i < length; i++)
                    item.Password += "*";
            }
            return list;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) // чтобы перетаскивать окно
        {
            if(e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) // при двойном нажатии выходит фулл экран
        {
            if(e.ClickCount == 2)
            {
                if(isMaximized)
                {
                    WindowState = WindowState.Normal;
                    Width = 1080;
                    Height = 720;
                    isMaximized = false;
                }
                else
                {
                    WindowState = WindowState.Maximized;
                    isMaximized = true;
                }
            }
        }

        // ALL DEFAULTS
        void setDefault()
        {
            setDefaultMainMenu();
            setDefaultAFC();
            setDefaultGT();
            setDefaultParticipants();
            setDefaultProfile();
        }

        private void EnlargeButton_Click(object sender, RoutedEventArgs e)
        {
            if(!isMaximized)
            {
                WindowState = WindowState.Maximized;
                isMaximized = true;
            }
            else
            {
                WindowState = WindowState.Normal;
                Width = 1080;
                Height = 720;
                isMaximized = false;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) => this.Close();
        private void ExitButton_MouseEnter(object sender, MouseEventArgs e) => ExitButton.Content = "\u2716";
        private void ExitButton_MouseLeave(object sender, MouseEventArgs e) => ExitButton.Content = "⨉";
        private void EnlargeButton_MouseEnter(object sender, MouseEventArgs e) => EnlargeButton.Content = "\u25A0";
        private void EnlargeButton_MouseLeave(object sender, MouseEventArgs e) => EnlargeButton.Content = "◻";
        private void RollUpButton_MouseEnter(object sender, MouseEventArgs e) => RollUpButton.Content = "\u2501";

        private void RollUpButton_MouseLeave(object sender, MouseEventArgs e) => RollUpButton.Content = "─";

        private void RollUpButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;   
    }
}