using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftModRename
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入設定json");
            var settingJson = File.ReadAllText(Console.ReadLine());
            Console.WriteLine("請輸入mods資料夾路徑");
            var modPath = Console.ReadLine();
            var setting = JToken.Parse(settingJson);
            var disableds = ((JArray)setting["disabled"]).Select(token => token.Value<string>()).ToList();
            var enableds = ((JArray)setting["enabled"]).Select(token => token.Value<string>() + ".disabled").ToList();
            var mods = Directory.GetFiles(modPath);
            foreach (var item in mods)
            {
                if (disableds.Contains(Path.GetFileName(item)))
                {
                    File.Move(item, item + ".disabled");
                    continue;
                }
                if (enableds.Contains(Path.GetFileName(item)))
                {
                    File.Move(item, item.Substring(0, item.Length - 9));
                    continue;
                }

            }
            Console.WriteLine("完成");
            Console.ReadKey();
        }
    }
}
