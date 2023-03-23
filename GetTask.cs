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

namespace Fight_Club;
public partial class Window1
{
    void Button_MouseLeave_2(object sender, MouseEventArgs e) => textBlockGetTask.Foreground = Brushes.White;
    void Button_MouseEnter_2(object sender, MouseEventArgs e) => textBlockGetTask.Foreground = buttonGT.Background == Brushes.LightGreen ? Brushes.White : Brushes.LightGreen;
    private void buttonGT_Click(object sender, RoutedEventArgs e)
    {
        setDefault();
        textBlockGetTask.Foreground = Brushes.White;
        buttonGT.Background = Brushes.LightGreen;
        textBlockChapterGetTask.Visibility = Visibility.Visible;
        Gettaskgrid.Visibility = Visibility.Visible;
    }
    void setDefaultGT()
    {
        buttonGT.Background = Brushes.Transparent;
        textBlockChapterGetTask.Visibility = Visibility.Hidden;
        Gettaskgrid.Visibility = Visibility.Hidden;
    }
    private async void buttonGenerate_Click(object sender, RoutedEventArgs e)
    {
        string read = File.ReadAllText("Tasks.txt");
        var res = JsonConvert.DeserializeObject<List<GetTask>>(read);

        buttonGenerate.IsEnabled = false;
        for(int i = 0; i < 2; i++)
        {
            foreach(var item in res!) // поставил так как предупреждение мешало
            {
                textBoxgenerateTask.Text = item.Tasks;
                await Task.Delay(50); // от этого логика никак не меняется, только украшает процесс анимации (Sleep не работал)
            }
            textBoxgenerateTask.Clear();
        }

        Random random = new Random();
        int index = random.Next(res!.Count);

        int counter = 0;
        foreach(var item in res)
        {
            if(counter == index)
            {
                textBoxgenerateTask.Text = item.Tasks;
                break;
            }
            counter++;
        }

        buttonGenerate.IsEnabled = true;
    }
}
class GetTask
{
    public string Tasks { get; set; }
    public GetTask(string tasks) => Tasks = tasks;
}