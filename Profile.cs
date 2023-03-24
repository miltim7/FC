using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace Fight_Club;
public partial class Window1
{
    bool isChanged = false;
    private string defaultImage = "https://cdn4.iconfinder.com/data/icons/small-n-flat/24/user-512.png";
    private string path = "characters.txt";

    private bool isUpploadPhotoPressed = false;
    void Button_MouseLeave_4(object sender, MouseEventArgs e) => textBlockProf.Foreground = Brushes.White;
    void Button_MouseEnter_4(object sender, MouseEventArgs e) => textBlockProf.Foreground = buttonProfile.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
    private void buttonProfile_Click(object sender, RoutedEventArgs e)
    {
        setDefault();

        var bitmap = new BitmapImage(new Uri(nowAvatar()));
        PhotoEllipse.Fill = new ImageBrush(bitmap);

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
    private string nowAvatar()
    {
        string read = System.IO.File.ReadAllText(path);
        var json = JsonConvert.DeserializeObject<List<User>>(read);
        foreach(var item in json)
        {
            if (item.Nickname == MainWindow.nowName)
            {
                if (!string.IsNullOrWhiteSpace(item.URL))
                    return item.URL;
                break;
            }
        }
        return defaultImage;
    }
    private void setUserData()
    {
        string read = System.IO.File.ReadAllText(path);
        var res = JsonConvert.DeserializeObject<List<User>>(read);
        int count = 0;
        foreach(var item in res)
        {
            if (item.Nickname == MainWindow.nowName)
            {
                textBoxUserNameProfile.Text = item.Nickname;
                break;
            }
        }
    }
    private string setShifrPassword(string password)
    {
        StringBuilder p = new StringBuilder(password.Length);
        for(int i = 0; i < password.Length; i++)
            p = p.Append("🞄");
        return p.ToString();
    }
    private void SaveChanges_Click(object sender, RoutedEventArgs e)
    {
        List<User> newUsers = new List<User>();

        string read = System.IO.File.ReadAllText(path);
        var jsonDes = JsonConvert.DeserializeObject<List<User>>(read);

        foreach(var item in jsonDes)
        {
            if (item.Nickname == MainWindow.nowName)
            {
                string newNick = textBoxUserNameProfile.Text;

                string newUrl = string.Empty;

                if(isChanged)
                    newUrl = UrlTextBox.Text;
                else
                {
                    newUrl = item.URL;
                }

                newUsers.Add(new User(newNick, item.Password, item.Id, newUrl));
                MainWindow.nowName = textBoxUserNameProfile.Text;
                var bitmap = new BitmapImage(new Uri(newUrl));
                topPanelImage.Fill = new ImageBrush(bitmap);
                nameShowLabel.Content = newNick;
            }
            else
            {
                newUsers.Add(new User(item.Nickname, item.Password, item.Id, item.URL));
            }
        }
        var jsonSer = JsonConvert.SerializeObject(newUsers);
        System.IO.File.WriteAllText(path, jsonSer);
        myListView.ItemsSource = LoadToGrid("characters.txt");
    }

    private void UrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            var bitmap = new BitmapImage(new Uri(UrlTextBox.Text));
            PhotoEllipse.Fill = new ImageBrush(bitmap);
        }
        catch(Exception) { }

        if(IsUrl(UrlTextBox.Text))
            saveChanges.IsEnabled = true;
        else
            saveChanges.IsEnabled = false;
        if(string.IsNullOrEmpty(UrlTextBox.Text))
            saveChanges.IsEnabled = true;
        isChanged = true;
    }
    public static bool IsUrl(string url)
    {
        Uri uriResult;
        return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }


    private void uploadPhoto_Click(object sender, RoutedEventArgs e)
    {
        isUpploadPhotoPressed = isUpploadPhotoPressed == true ? false : true;

        if (!isUpploadPhotoPressed)
            borderUrlTextBox.Visibility = Visibility.Hidden;
        else
            borderUrlTextBox.Visibility = Visibility.Visible;
    }

    private bool checkExist(string nick) // проверяет на существование пользователя с таким же ником
    {
        if(System.IO.File.ReadAllText(path).Any())
        {
            var read = System.IO.File.ReadAllText(path);
            var res = JsonConvert.DeserializeObject<List<User>>(read);

            foreach(var item in res!)
            {
                if(item.Nickname == nick)
                    return true;
            }
        }

        return false;
    }
    private void textBoxUserNameProfile_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(checkExist(textBoxUserNameProfile.Text) && textBoxUserNameProfile.Text != MainWindow.nowName)
        {
            labelErrorNickname.Content = "Error: this nickname has already exist!";
            saveChanges.IsEnabled = false;
        }
        else
        {
            labelErrorNickname.Content = string.Empty;
            saveChanges.IsEnabled = true;
        }
    }

    private void LeaveTheClubButton_Click(object sender, RoutedEventArgs e)
    {
        string read = System.IO.File.ReadAllText("characters.txt");
        var json = JsonConvert.DeserializeObject<List<User>>(read);
        List<User> users = new List<User>();
        foreach(var item in json)
        {
            if(item.Nickname != MainWindow.nowName)
                users.Add(new User(item.Nickname, item.Password, item.Id, item.URL));
        }

        var jsonSer = JsonConvert.SerializeObject(users);
        System.IO.File.WriteAllText("characters.txt", jsonSer);
        this.Close();
    }

    private void LeaveButton_Click(object sender, RoutedEventArgs e)
    {
        areYouSureText.Visibility = Visibility.Visible;
        YesButton.Visibility = Visibility.Visible;
        NoButton.Visibility = Visibility.Visible;
    }

    private void NoButton_Click(object sender, RoutedEventArgs e)
    {
        areYouSureText.Visibility = Visibility.Hidden;
        YesButton.Visibility = Visibility.Hidden;
        NoButton.Visibility = Visibility.Hidden;
    }

    private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
    {
        Window3 window3 = new Window3();
        window3.ShowDialog();
    }
}