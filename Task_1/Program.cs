using System;
using System.Collections.Generic;
using System.IO;

namespace Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo fi = new DirectoryInfo(args[0]);
            List<FileInfo> fileList = new List<FileInfo>();
            TimeSpan idleInterval = TimeSpan.FromMinutes(30);
            foreach (FileInfo file in fi.GetFiles())
            {
                if ((DateTime.Now - file.LastAccessTime) > idleInterval)
                {
                    fileList.Add(file);
                }
            }
            
            Console.WriteLine($"Найдено {fileList.Count} файла.{Environment.NewLine}");
            Console.WriteLine($"[Esc] - выход, [Del] - удалить, [Enter] - показать");
            
            ConsoleKeyInfo consoleKey;
            do {
                do {
                    consoleKey = Console.ReadKey();
                }
                while (!ConsoleKey.Escape.Equals(consoleKey.Key) && 
                     !ConsoleKey.Enter.Equals(consoleKey.Key) && 
                     !ConsoleKey.Delete.Equals(consoleKey.Key));
                switch (consoleKey.Key)
                {
                    case ConsoleKey.Delete:
                        foreach(FileInfo file in fileList)
                            { file.Delete(); };
                        break;
                    case ConsoleKey.Enter:
                        foreach (FileInfo file in fileList)
                        { Console.WriteLine(file.Name); };
                        break;
                    case ConsoleKey.Escape:
                        break;
                };
            }
            while (!ConsoleKey.Escape.Equals(consoleKey.Key) &&
            !ConsoleKey.Delete.Equals(consoleKey.Key));
            return;
        }
    }
}
