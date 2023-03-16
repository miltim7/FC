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
        public int Rating { get; set; }
        public Window1()
        {
            InitializeComponent();

            myListView.ItemsSource = LoadToGrid("characters.txt");
            nameShowLabel.Content = MainWindow.nowName;
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

        // Left Menu Buttons Color Changes
        void Button_MouseLeave(object sender, MouseEventArgs e) => textBlockMainMenu.Foreground = Brushes.White;
        void Button_MouseEnter(object sender, MouseEventArgs e) => textBlockMainMenu.Foreground = buttonMainMenu.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
        void Button_MouseLeave_1(object sender, MouseEventArgs e) => textBlockAb.Foreground = Brushes.White;
        void Button_MouseEnter_1(object sender, MouseEventArgs e) => textBlockAb.Foreground = buttonAFC.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
        void Button_MouseLeave_2(object sender, MouseEventArgs e) => textBlockGetTask.Foreground = Brushes.White;
        void Button_MouseEnter_2(object sender, MouseEventArgs e) => textBlockGetTask.Foreground = buttonGT.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
        void Button_MouseLeave_3(object sender, MouseEventArgs e) => textBlockPar.Foreground = Brushes.White;
        void Button_MouseEnter_3(object sender, MouseEventArgs e) => textBlockPar.Foreground = buttonParticipants.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
        void Button_MouseLeave_4(object sender, MouseEventArgs e) => textBlockProf.Foreground = Brushes.White;
        void Button_MouseEnter_4(object sender, MouseEventArgs e) => textBlockProf.Foreground = buttonProfile.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
        void Button_MouseLeave_5(object sender, MouseEventArgs e) => textBlockLogOut.Foreground = Brushes.White;
        void Button_MouseEnter_5(object sender, MouseEventArgs e) => textBlockLogOut.Foreground = Brushes.LightGreen;

        private void Button_Click_1(object sender, RoutedEventArgs e) => this.Close();


        // Main Menu
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            setDefault();
            textBlockMainMenu.Foreground = Brushes.White;
            buttonMainMenu.Background = Brushes.LightGreen;
            textBlockChapterMainMenu.Visibility = Visibility.Visible;
        }
        void setDefaultMainMenu()
        {
            buttonMainMenu.Background = Brushes.Transparent;
            textBlockChapterMainMenu.Visibility = Visibility.Hidden;
        }

        // About Fight Club
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            setDefault();
            textBlockAb.Foreground = Brushes.White;
            buttonAFC.Background = Brushes.LightGreen;
            string text = File.ReadAllText("About Fight Club.txt");
            textBlockChapterAboutFC.Visibility = Visibility.Visible;
            textBlockAboutFCtext.Visibility = Visibility.Visible;
            textBlockAboutFCtext.Text = text;
        }
        void setDefaultAFC()
        {
            buttonAFC.Background = Brushes.Transparent;
            textBlockChapterAboutFC.Visibility = Visibility.Hidden;
            textBlockAboutFCtext.Visibility = Visibility.Hidden;
        }


        // Get Taks
        private void buttonGT_Click(object sender, RoutedEventArgs e)
        {
            setDefault();
            textBlockGetTask.Foreground = Brushes.White;
            buttonGT.Background = Brushes.LightGreen;
            textBlockChapterGetTask.Visibility = Visibility.Visible;
            textBlockRandomText.Visibility = Visibility.Visible;
            textBoxgenerateTask.Visibility = Visibility.Visible;
            textBlockSeparator.Visibility = Visibility.Visible;
            buttonGenerate.Visibility = Visibility.Visible;
        }
        void setDefaultGT()
        {
            buttonGT.Background = Brushes.Transparent;
            textBlockChapterGetTask.Visibility = Visibility.Hidden;
            textBlockRandomText.Visibility = Visibility.Hidden;
            textBoxgenerateTask.Visibility = Visibility.Hidden;
            textBlockSeparator.Visibility = Visibility.Hidden;
            buttonGenerate.Visibility = Visibility.Hidden;
        }
        private async void buttonGenerate_Click(object sender, RoutedEventArgs e) // GENERATE
        {
            string read = File.ReadAllText("Tasks.txt");
            var res = JsonConvert.DeserializeObject<List<GetTask>>(read);

            buttonGenerate.IsEnabled = false;
            for(int i = 0; i < 2; i++)
            {
                foreach(var item in res!) // поставил так как предупреждение мешало
                {
                    textBoxgenerateTask.Text = item.Tasks;
                    await Task.Delay(50);
                }
                textBoxgenerateTask.Clear();
            }

            Random random = new Random();
            int index = random.Next(res!.Count);

            int counter = 0;
            foreach(var item in res)
            {
                if(counter == index)
                {
                    textBoxgenerateTask.Text = item.Tasks;
                    break;
                }
                counter++;
            }

            buttonGenerate.IsEnabled = true;
        }

        // Participants
        private void buttonParticipants_Click(object sender, RoutedEventArgs e)
        {
            setDefault();
            textBlockPar.Foreground = Brushes.White;
            buttonParticipants.Background = Brushes.LightGreen;
            textBlockChapterParticipants.Visibility = Visibility.Visible;
            GridParticipants.Visibility = Visibility.Visible;
        }
        void setDefaultParticipants()
        {
            buttonParticipants.Background = Brushes.Transparent;
            textBlockChapterParticipants.Visibility = Visibility.Hidden;
            GridParticipants.Visibility = Visibility.Hidden;
        }

        // Profile
        private void buttonProfile_Click(object sender, RoutedEventArgs e)
        {
            setDefault();
            textBlockProf.Foreground = Brushes.White;
            buttonProfile.Background = Brushes.LightGreen;
            textBlockChapterProfile.Visibility = Visibility.Visible;
        }
        void setDefaultProfile()
        {
            buttonProfile.Background = Brushes.Transparent;
            textBlockChapterProfile.Visibility = Visibility.Hidden;
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

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) // ID changed
        {
            string read = File.ReadAllText("characters.txt");
            var res = JsonConvert.DeserializeObject<List<User>>(read);

            IdTextBox.Text = IdTextBox.Text.Trim();

            int number = 0;
            var num = int.TryParse(IdTextBox.Text, out number);
            if(number < 1 || number > res?.Count)
            {
                BorderWithoutId.Visibility = Visibility.Hidden;
                return;
            }

            for(int i = 0; i < IdTextBox.Text.Length; i++)
                if(!char.IsDigit(IdTextBox.Text[i]))
                {
                    BorderWithoutId.Visibility = Visibility.Hidden;
                    return;
                }


            string name = string.Empty;
            string rating = string.Empty;
            string image = string.Empty;

            foreach(var item in res)
            {
                if(item.Id == number.ToString())
                {
                    name = item.Nickname;
                    rating = "0";
                    image = "https://cdn4.iconfinder.com/data/icons/small-n-flat/24/user-512.png";
                }
            }

            File.WriteAllText("images.txt", image);
            ratingPoint.Text = rating;
            nameInProp.Text = name;
            BorderWithoutId.Visibility = Visibility.Visible;
        }
    }

    class GetTask
    {
        public string Tasks { get; set; }
        public GetTask(string tasks)
        {
            Tasks = tasks;
        }
    }
}

class Images
{
    static public List<Images> list = new List<Images>();
    string path = "images.txt";
    public string Id { get; set; }
    public string Image { get; set; }
    public Images(string image, string id)
    {
        Image = image;
        Id = id;
    }

    public static void SafeImage()
    {
        var json = JsonConvert.SerializeObject(list);
    }
}