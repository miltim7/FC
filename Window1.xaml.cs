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
using System.Windows.Shapes;

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
        }

        private List<User> LoadToGrid(string path)
        {
            var read = File.ReadAllText(path);
            var list = JsonConvert.DeserializeObject<List<User>>(read);
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
    }
}