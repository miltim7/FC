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
        string nowPhoto = string.Empty;
        foreach(var item in res)
        {
            if(item.Id == number.ToString())
            {
                name = item.Nickname;
                rating = "0";
                nowPhoto = item.URL;
            }
        }

        if(string.IsNullOrWhiteSpace(nowPhoto))
            nowPhoto = defaultImage;
        var bitmap = new BitmapImage(new Uri(nowPhoto));
        imageInProp.Fill = new ImageBrush(bitmap);

        ratingPoint.Text = rating;
        nameInProp.Text = name;
        BorderWithoutId.Visibility = Visibility.Visible;
    }
}