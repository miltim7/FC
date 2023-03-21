// RECHANGE 1

using MahApps.Metro.Markup;
using Microsoft.Win32;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics.PerformanceData;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;

namespace Fight_Club;

class User
{
    public string Id { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }
    public string Rating { get; set; }
    public string URL { get; set; }
    public User(string nickname, string password, string id, string url)
    {

        Nickname = nickname;
        Password = password;
        Id = id;
        URL = url;
    }
}
public partial class MainWindow : Window
{
    string path = "characters.txt";
    static public string nowName { get; set; }
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e) // LOG IN
    {
        if(checkExist(nickname => nickname == nicknameBox.Text) == false)
        {
            error1.Content = "This user does not exist!";
            return;
        }
        else
            error1.Content = string.Empty;
        if(string.IsNullOrWhiteSpace(nicknameBox.Text))
        {
            error1.Content = "This field should not be empty or filled with spaces!";
            return;
        }
        else
            error1.Content = string.Empty;
        if(string.IsNullOrWhiteSpace(passwordBox.Password))
        {
            error2.Content = "This field should not be empty or filled with spaces!";
            return;
        }
        else
            error2.Content = string.Empty;

        if(searchPasswordByName(nicknameBox.Text) != passwordBox.Password)
        {
            error2.Content = "Wrong password!";
            return;
        }
        else
            error2.Content = string.Empty;
        nowName = nicknameBox.Text;
        Window1 window1 = new Window1();
        this.Close();
        window1.ShowDialog();
    }

    private void Button_Click(object sender, RoutedEventArgs e) // SIGN UP
    {
        if(checkExist(nickname => nickname == nicknameBox.Text) == true)
        {
            error1.Content = "This name has already been used!";
            return;
        }
        else
            error1.Content = string.Empty;
        if(string.IsNullOrWhiteSpace(nicknameBox.Text))
        {
            error1.Content = "This field should not be empty or filled with spaces!";
            return;
        }
        else
            error1.Content = string.Empty;
        if(string.IsNullOrWhiteSpace(passwordBox.Password))
        {
            error2.Content = "This field should not be empty or filled with spaces!";
            return;
        }
        else
            error2.Content = string.Empty;

        List<User> users = new List<User>();
        int countUser = CountUsers() + 1;
        users.Add(new User(nicknameBox.Text, passwordBox.Password, countUser.ToString(), ""));
        SafeUser(users);

        nowName = nicknameBox.Text;
        Window1 window1 = new Window1();
        this.Close();
        window1.ShowDialog();
    }

    int CountUsers()
    {
        var read = File.ReadAllText(path);
        var res = JsonConvert.DeserializeObject<List<User>>(read);
        return res.Count;
    }
    private void SafeUser(List<User> users)
    {
        var json = JsonConvert.SerializeObject(users);

        string check = File.ReadAllText(path);

        if(check.Any())
        {
            StringBuilder sb = new StringBuilder(json.Length);
            sb = sb.Append(File.ReadAllText(path));
            File.Delete(path);
            sb = sb.Remove(sb.Length - 1, 1);
            sb = sb.Append($",{json.Remove(0, 1)}");
            File.AppendAllText(path, sb.ToString());
        }

        if(!check.Any())
            File.AppendAllText(path, json);
    }

    private string searchPasswordByName(string name)
    {
        if(File.ReadAllText(path).Any())
        {
            var read = File.ReadAllText(path);
            var res = JsonConvert.DeserializeObject<List<User>>(read);

            foreach(var item in res!)
            {
                if(item.Nickname == name)
                    return item.Password;
            }
        }

        return string.Empty;
    }

    private bool checkExist(Func<string, bool> predicate) // проверяет на существование пользователя с таким же ником
    {
        if(File.ReadAllText(path).Any())
        {
            var read = File.ReadAllText(path);
            var res = JsonConvert.DeserializeObject<List<User>>(read);

            foreach(var item in res!)
            {
                if(predicate(item.Nickname))
                    return true;
            }
        }

        return false;
    }
}