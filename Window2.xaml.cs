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
    public partial class Window2 : Window
    {
        private string password = "qwerty";
        public Window2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.ToString() != password)
            {
                errorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                errorLabel.Visibility = Visibility.Hidden;
                setButton.IsEnabled = false;
                panelAfterSet.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < IdTextBox.Text.ToString().Length; i++)
            {
                if(!char.IsDigit(IdTextBox.Text.ToString()[i]))
                {
                    errorLabel2.Content = "id must be number";
                    return;
                }
                else
                {
                    int count = CountPart();
                    int num = int.Parse(IdTextBox.Text.ToString());
                    if (num < 1)
                    {
                        errorLabel2.Content = "number cant be < 0";
                        return;
                    }
                    else if (!IsSuchId(num))
                    {
                        errorLabel2.Content = $"such an ID does not exist";
                        return;
                    }
                    else
                    {
                        DeleteUser(num);
                        this.Close();
                    }
                }
            }
        }

        private bool IsSuchId(int id)
        {
            string read = File.ReadAllText("characters.txt");

            var json = JsonConvert.DeserializeObject<List<User>>(read);

            foreach(var item in json)
            {
                if (item.Id == id.ToString())
                    return true;
            }

            return false;
        }
        private int CountPart()
        {
            string read = File.ReadAllText("characters.txt");
            var json = JsonConvert.DeserializeObject<List<User>>(read);
            return json.Count;
        }

        private void DeleteUser(int id)
        {
            string read = File.ReadAllText("characters.txt");
            var json = JsonConvert.DeserializeObject<List<User>>(read);
            List<User> users = new List<User>();
            foreach(var item in json)
            {
                if (item.Id != id.ToString())
                    users.Add(new User(item.Nickname, item.Password, item.Id, item.URL));
            }

            var jsonSer = JsonConvert.SerializeObject(users);
            File.WriteAllText("characters.txt", jsonSer);
        }
    }
}
