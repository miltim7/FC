using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fight_Club;

static class GeneralOptions
{
    static public List<User> GetUsers()
    {
        var read = File.ReadAllText("characters.txt");
        List<User> json = JsonConvert.DeserializeObject<List<User>>(read);
        return json;
    }
}
