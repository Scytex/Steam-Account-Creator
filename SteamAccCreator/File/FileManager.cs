using System;
using System.IO;
using System.Threading.Tasks;
using SteamAccCreator.Gui;

namespace SteamAccCreator.File
{
    public class FileManager
    {
        public void WriteIntoFile(string mail, bool writeMail, string alias, string pass, string path, string style)
        {

            Task.Delay(2000).Wait();

            using (var writer = new StreamWriter(path, true))
            {
                if (style == "User:Pass Formatting")
                {
                    if (writeMail == false)
                    {
                        writer.WriteLine(alias + ":" + pass);
                    }
                    else
                    {
                        writer.WriteLine(alias + ":" + pass + ":" + mail);
                    }
                } else if (style == "Original Formatting")
                {
                    if (writeMail == false)
                    {
                        writer.WriteLine("Alias: \t\t" + alias);
                        writer.WriteLine("Pass: \t\t" + pass);
                        writer.WriteLine("Creation: \t" + DateTime.Now);
                        writer.WriteLine("###########################");
                    }
                    else
                    {
                        writer.WriteLine("Mail: \t\t" + mail);
                        writer.WriteLine("Alias: \t\t" + alias);
                        writer.WriteLine("Pass: \t\t" + pass);
                        writer.WriteLine("Creation: \t" + DateTime.Now);
                        writer.WriteLine("###########################");
                    }
                }
            }
        }
    }
}
