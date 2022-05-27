using System;
using System.Collections.Generic;
using System.IO;

namespace Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try //обработчик ошибки общего доступа к папке
            {
                DirectoryInfo fi = new DirectoryInfo(args[0]);
                if (!fi.Exists)
                {
                    Console.WriteLine("папка не сучествует");
                    return;
                };
                List<FileInfo> fileList = new List<FileInfo>(); //список файлов для отложенного удаления
                List<DirectoryInfo> dirList = new List<DirectoryInfo>(); //список директорий для отложенного удаления
                TimeSpan timeSpentIdle = TimeSpan.FromMinutes(30);

                seekObject(timeSpentIdle, dirList, fileList, fi);
            
                ConsoleKeyInfo consoleKey;

                do {
                    Console.Clear();//выводим интерактивное меню
                    Console.WriteLine($"Найдено {fileList.Count} файла.{Environment.NewLine}");
                    Console.WriteLine($"[Esc] - выход, [Del] - рекурсивное удалиение файлов и папок, [Enter] - показать");
                    do {//зацикливаем ввод для завершения выбора из предложенных вариантов
                        consoleKey = Console.ReadKey();
                    }
                    while (!ConsoleKey.Escape.Equals(consoleKey.Key) && 
                         !ConsoleKey.Enter.Equals(consoleKey.Key) && 
                         !ConsoleKey.Delete.Equals(consoleKey.Key));
                    switch (consoleKey.Key)//
                    {
                        case ConsoleKey.Delete:
                            try//обработчик ошибки на этапе удаления
                            {
                            foreach(FileInfo file in fileList)
                                { file.Delete(); };
                            foreach(DirectoryInfo dir in dirList)
                                {
                                    if ((dir.GetFiles().Length == 0) && 
                                        (dir.GetDirectories().Length == 0))
                                    { 
                                        dir.Delete(); 
                                    }
                                };
                            }
                            catch ( Exception e ) // вывод сообщения об ошибке удаления
                            {
                                Console.WriteLine($"ошибка удаления ... {e.Message}");
                            }
                            break;
                        case ConsoleKey.Enter:
                            foreach (FileInfo file in fileList)
                            { Console.WriteLine(file.FullName); };
                            Console.WriteLine("?... нажмите любую клавишу");
                            Console.ReadKey();
                            break;
                        case ConsoleKey.Escape:
                            break;
                    };
                }
                while (!ConsoleKey.Escape.Equals(consoleKey.Key) &&
                !ConsoleKey.Delete.Equals(consoleKey.Key));
                return;
            }
            catch(Exception e)
            { Console.WriteLine($"нет доступа... {e.Message}"); }
        }
        public static void seekObject(TimeSpan timeAgo, List<DirectoryInfo> listDirs, List<FileInfo> listFiles, DirectoryInfo path)
        {
            foreach (FileInfo file in path.GetFiles())
            {
                if ((DateTime.Now - file.LastAccessTime) > timeAgo)
                {
                    listFiles.Add(file);
                }
            }
            foreach (DirectoryInfo dir in path.GetDirectories())
            {
                if ((DateTime.Now - dir.LastAccessTime) > timeAgo)
                {
                    listDirs.Add(dir);
                    seekObject(timeAgo, listDirs, listFiles, dir);
                }
            }
        }
    }
}
