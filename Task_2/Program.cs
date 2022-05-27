using System;
using System.IO;

namespace Task_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long foldersize;
            try //обработчик ошибки общего доступа к папке
            {
                DirectoryInfo fi = new DirectoryInfo(args[0]);
                if (!fi.Exists)
                {
                    Console.WriteLine("папка не существует");
                    return;
                };
                foldersize = SumSizeFolder(fi);
                Console.WriteLine($"Размер папки {foldersize} байт.");
                return;
            }
            catch (Exception e)
            { Console.WriteLine($"нет доступа... {e.Message}"); }

        }
        public static long SumSizeFolder(DirectoryInfo path)
        {
            long sum = 0;
            foreach (FileInfo file in path.GetFiles())
            {
                sum += file.Length;
            }
            foreach (DirectoryInfo dir in path.GetDirectories())
            {
                sum += SumSizeFolder(dir);
            }
            return sum;
        }
    }
}
