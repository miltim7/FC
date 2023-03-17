using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fight_Club;
public partial class Window1
{
    string path = "characters.txt"; 
    private bool isUpploadPhotoPressed = false;
    void Button_MouseLeave_4(object sender, MouseEventArgs e) => textBlockProf.Foreground = Brushes.White;
    void Button_MouseEnter_4(object sender, MouseEventArgs e) => textBlockProf.Foreground = buttonProfile.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
    private void buttonProfile_Click(object sender, RoutedEventArgs e)
    {
        setDefault();
        textBlockProf.Foreground = Brushes.White;
        buttonProfile.Background = Brushes.LightGreen;
        textBlockChapterProfile.Visibility = Visibility.Visible;
        borderProfile.Visibility = Visibility.Visible;
        setUserData();
    }
    private void setDefaultProfile()
    {
        buttonProfile.Background = Brushes.Transparent;
        textBlockChapterProfile.Visibility = Visibility.Hidden;
        borderProfile.Visibility = Visibility.Hidden;
    }
    private void setUserData()
    {
        string read = File.ReadAllText("characters.txt");
        var res = JsonConvert.DeserializeObject<List<User>>(read);

        foreach(var item in res)
        {
            if (item.Nickname == MainWindow.nowName)
            {
                textBoxUserNameProfile.Text = item.Nickname;
                textBoxUserPasswordProfile.Text = item.Password;
                break;
            }
        }
    }
    private void SaveChanges_Click(object sender, RoutedEventArgs e)
    {

    }


    private void uploadPhoto_Click(object sender, RoutedEventArgs e)
    {
        isUpploadPhotoPressed = isUpploadPhotoPressed == true ? false : true;

        if (isUpploadPhotoPressed)
            borderUrlTextBox.Visibility = Visibility.Visible;
        else
            borderUrlTextBox.Visibility = Visibility.Hidden;
    }

    private void UrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}
