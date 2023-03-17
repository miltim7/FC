using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Fight_Club;
public partial class Window1
{
    void Button_MouseLeave_1(object sender, MouseEventArgs e) => textBlockAb.Foreground = Brushes.White;
    void Button_MouseEnter_1(object sender, MouseEventArgs e) => textBlockAb.Foreground = buttonAFC.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
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
}
