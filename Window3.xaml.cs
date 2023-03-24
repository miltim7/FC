using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fight_Club
{
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private void passwordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string read = File.ReadAllText("characters.txt");
            var json = JsonConvert.DeserializeObject<List<User>>(read);

            foreach(var item in json)
            {
                if (item.Nickname == MainWindow.nowName)
                {
                    if(item.Password == oldPassword.Password)
                    {
                        error1.Visibility = Visibility.Hidden;
                        newPassword.IsEnabled = true;
                        oldPassword.IsEnabled = false;
                        break;
                    }
                    else
                    {
                        error1.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            List<User> newUsers = new List<User>();
            string read = File.ReadAllText("characters.txt");
            var json = JsonConvert.DeserializeObject<List<User>>(read);

            foreach(var item in json)
            {
                if (item.Password == oldPassword.Password)
                    newUsers.Add(new User(item.Nickname, newPassword.Password, item.Id, item.URL));
                else
                    newUsers.Add(new User(item.Nickname, item.Password, item.Id, item.URL));
            }

            var jsonSer = JsonConvert.SerializeObject(newUsers);
            File.WriteAllText("characters.txt", jsonSer);
            this.Close();
        }

        private void newPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(newPassword.Password))
                button.IsEnabled = true;
        }
    }
}
