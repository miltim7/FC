using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fight_Club;
public partial class Window1
{
    void Button_MouseLeave_3(object sender, MouseEventArgs e) => textBlockPar.Foreground = Brushes.White;
    void Button_MouseEnter_3(object sender, MouseEventArgs e) => textBlockPar.Foreground = buttonParticipants.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
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
    private void deleteButton_Click(object sender, RoutedEventArgs e)
    {
        Window2 window2 = new Window2();
        window2.Show();
    }
}