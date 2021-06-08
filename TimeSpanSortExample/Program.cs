using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace TimeSpanSortExample
{
    class Program
    {
        static void Main()
        {
            var path = @"C:\temp\timespantest.txt";
            FileInfo file = new FileInfo(path);

            //Create file with existing timestamps if not already in existence and populated
            if (File.Exists(path))
            {
                if (file.Length == 0)
                {
                    GenerateTimeStamps();
                }
            }
            else
            {
                GenerateTimeStamps();
            }

            var duration = TimeSpan.FromMilliseconds(DateTime.Now.Millisecond);
            WriteText(path, duration);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static void WriteText(string strPath, TimeSpan tsDuration)
        {
            string line;
            var list = new List<TimeSpan>();

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Reading list from file...");
            using (var sr = new StreamReader(strPath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    list.Add(TimeSpan.Parse(line));
                }
            }

            list.Add(tsDuration);
            list.OrderByDescending(i => i);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Writing list to file...");
            using StreamWriter str = new StreamWriter(strPath, true);
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());    
                str.WriteLine(item);
            }
        }

        private static void GenerateTimeStamps()
        {
            for (var i = 0; i <= 20; i++)
            {
                var path = @"C:\temp\timespantest.txt";
                var duration = TimeSpan.FromMilliseconds(DateTime.Now.Millisecond);
                PopulateTestFile(path, duration);
                Thread.Sleep(1000);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void PopulateTestFile(string strPath, TimeSpan tsDuration)
        {
            Console.WriteLine("writing value: {0} to file", tsDuration);
            using StreamWriter str = new StreamWriter(strPath, true);
            str.WriteLine(tsDuration);
        }
    }
}