using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Fight_Club;
public partial class Window1
{
    void Button_MouseLeave(object sender, MouseEventArgs e) => textBlockMainMenu.Foreground = Brushes.White;
    void Button_MouseEnter(object sender, MouseEventArgs e) => textBlockMainMenu.Foreground = buttonMainMenu.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        setDefault();
        textBlockMainMenu.Foreground = Brushes.White;
        buttonMainMenu.Background = Brushes.LightGreen;
        textBlockChapterMainMenu.Visibility = Visibility.Visible;
        gridMainMenu.Visibility = Visibility.Visible;
        setNowDate();
    }
    private void setNowDate()
    {
        DateTime date = DateTime.Now;
        textBlockDate.Text = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}";
        textBlockWeekday.Text = date.DayOfWeek.ToString();

        // it's for update time every second
        var timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        timer.Start();
        Timer_Tick(null, null);
    }
    private void Timer_Tick(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        textBlockTime.Text = now.ToString("HH:mm:ss tt");
    }

    void setDefaultMainMenu()
    {
        buttonMainMenu.Background = Brushes.Transparent;
        textBlockChapterMainMenu.Visibility = Visibility.Hidden;
        gridMainMenu.Visibility = Visibility.Hidden;
    }

    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        mainBorder.Background = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#eff2f7"));
        leftPanelBorder.Background = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#623ed0"));

        textBoxgenerateTask.Foreground = Brushes.Black;
        textBlockAboutFCtext.Foreground = Brushes.Black;
        textBoxgenerateTask.Foreground = Brushes.Black;
        participantsMainBorder.Background = Brushes.Transparent;

        textBlockChapterGetTask.Foreground = Brushes.Black;
        textBlockChapterMainMenu.Foreground = Brushes.Black;
        textBlockChapterParticipants.Foreground = Brushes.Black;
        textBlockChapterProfile.Foreground = Brushes.Black;
        textBlockChapterAboutFC.Foreground = Brushes.Black;
    }

    private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        mainBorder.Background = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#1f1d16"));
        leftPanelBorder.Background = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#2c0f4a"));

        textBoxgenerateTask.Foreground = Brushes.White;
        participantsMainBorder.Background = Brushes.WhiteSmoke;
        textBlockAboutFCtext.Foreground = Brushes.White;
        textBlockRandomText.Foreground = Brushes.White;

        textBlockChapterGetTask.Foreground = Brushes.White;
        textBlockChapterMainMenu.Foreground = Brushes.White;
        textBlockChapterParticipants.Foreground = Brushes.White;
        textBlockChapterProfile.Foreground = Brushes.White;
        textBlockChapterAboutFC.Foreground = Brushes.White;
    }
    private void musicButton_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "cmd",
            Arguments = $"/c start https://youtu.be/FSCgfI3OG7s",
            UseShellExecute = false,
            CreateNoWindow = true
        });
    }
}