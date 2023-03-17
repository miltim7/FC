using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
    }
    void setDefaultMainMenu()
    {
        buttonMainMenu.Background = Brushes.Transparent;
        textBlockChapterMainMenu.Visibility = Visibility.Hidden;
    }
}