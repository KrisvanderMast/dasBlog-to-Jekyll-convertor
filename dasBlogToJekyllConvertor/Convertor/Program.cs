using Convertor.Support;
using Html2Markdown;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Convertor
{
    class Program
    {
        private const string OUTPUTPATH = "_posts";

        static void Main(string[] args)
        {
            string path = args[0];

            ConsoleSetup();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var dasBlogEntries = ReadIndasBlogEntries(path);
            CrunchToJekyllPosts(dasBlogEntries, $"{path}\\{OUTPUTPATH}");

            sw.Stop();
            Console.WriteLine($"It took {sw.ElapsedMilliseconds}ms to convert {dasBlogEntries.Count} entries.");

            Console.ReadLine();
        }

        private static ICollection<DayEntryEntry> ReadIndasBlogEntries(string path)
        {
            var files = Directory.GetFiles(path).Where(name => name.EndsWith("dayentry.xml"));
            var dayEntries = new List<DayEntryEntry>();

            foreach (string filePath in files)
            {
                var serializer = new XmlSerializer(typeof(DayEntry));
                DayEntry entry = (DayEntry)serializer.Deserialize(new XmlTextReader(filePath));
                dayEntries.AddRange(entry.Entries.Select(item => item));
            }

            return dayEntries;
        }

        private static void CrunchToJekyllPosts(IEnumerable<DayEntryEntry> dasBlogEntries, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
                {
                    file.Delete();
                }
            }

            int counter = 1;

            foreach (var entry in dasBlogEntries)
            {
                ProgressBar.DrawTextProgressBar(counter++, dasBlogEntries.Count());
                DateTime entryDate = entry.Created;
                string jekyllFileName = $"{entry.Created.ToString("yyyy-MM-dd")}-{SanitizeTitle(entry.Title)}.md";

                using (var sw = new StreamWriter($"{path}\\{jekyllFileName}"))
                {
                    sw.WriteLine("---");
                    sw.WriteLine($"layout: post");
                    sw.WriteLine($"title: \"{entry.Title}\"");
                    sw.WriteLine($"date: 2017-03-29 17:12:00 +0200");
                    sw.WriteLine($"comments: true");
                    sw.WriteLine($"published: true");
                    sw.WriteLine($"categories: [\"post\"]");
                    sw.WriteLine($"tags: [{SanitizeCategories(entry.Categories)}]");
                    sw.WriteLine($"author: Kris van der Mast");
                    sw.WriteLine("---");
                    sw.WriteLine(SanitizeContent(entry.Content));
                }
            }
        }

        private static string SanitizeContent(string content)
        {
            var converter = new Converter();
            return converter.Convert(converter.Convert(content));
        }

        private static string SanitizeCategories(string categories)
        {
            if (categories == null)
            {
                categories = "";
            }
            var c = string.Join(",", categories?.Replace("|", " ").Split(";", StringSplitOptions.RemoveEmptyEntries).Select(item => "\"" + item + "\""));
            return c;
        }

        private static string SanitizeTitle(string title)
        {
            return Regex.Replace(title.ToLower(), "[^a-z ]", "").Replace(" ", "-");
        }

        private static void ConsoleSetup()
        {
            Console.Title = "dasBlog to Jekyll convertor";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"     .___             __________.__                    __                ____.       __           .__  .__   ");
            Console.WriteLine(@"   __| _/____    _____\______   \  |   ____   ____   _/  |_  ____       |    | ____ |  | _____.__.|  | |  |  ");
            Console.WriteLine(@"  / __ |\__  \  /  ___/|    |  _/  |  /  _ \ / ___\  \   __\/  _ \      |    |/ __ \|  |/ <   |  ||  | |  |  ");
            Console.WriteLine(@" / /_/ | / __ \_\___ \ |    |   \  |_(  <_> ) /_/  >  |  | (  <_> ) /\__|    \  ___/|    < \___  ||  |_|  |__");
            Console.WriteLine(@" \____ |(____  /____  >|______  /____/\____/\___  /   |__|  \____/  \________|\___  >__|_ \/ ____||____/____/");
            Console.WriteLine(@"      \/     \/     \/        \/           /_____/                                \/     \/\/                ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
