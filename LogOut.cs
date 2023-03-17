using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace Fight_Club;
public partial class Window1
{
    void Button_MouseLeave_5(object sender, MouseEventArgs e) => textBlockLogOut.Foreground = Brushes.White;
    void Button_MouseEnter_5(object sender, MouseEventArgs e) => textBlockLogOut.Foreground = Brushes.LightGreen;
    private void Button_Click_1(object sender, RoutedEventArgs e) => this.Close();
}
