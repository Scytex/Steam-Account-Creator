using System;
using System.Collections.Generic;
using System.IO;

namespace SteamAccCreator.File
{
    public class FileManager
    {
        private const string Path = @"accounts.txt";

        public void WriteIntoFile(string mail, string alias, string pass)
        {
            using (var writer = new StreamWriter(Path, true))
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