using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student[] students = new Student[0];
            BinaryFormatter formatter = new BinaryFormatter();
            //var  Names = new List<string>();
            var  Groups = new Dictionary<string, List<string>>();

            try
            {
                using (var fs = new FileStream($"C:\\Users\\{Environment.UserName}\\Desktop\\Students.dat", FileMode.Open))
                {
                    students = (Student[])formatter.Deserialize(fs);
                };
                
                for(int i = 0; i < students.Length; i++)
                {
                    if (!Groups.TryAdd(students[i].Group, new List<string>{ students[i].Name}))
                    {
                        Groups[students[i].Group].Add(students[i].Name) ;
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"При чтении файла возникла ошибка: ...\"{e.Message}\"");
            };
            try
            {
                DirectoryInfo dir = new DirectoryInfo($"C:\\Users\\{Environment.UserName}\\Desktop\\Student");
                if(dir.Exists)
                { 
                    Console.WriteLine($"Директория {dir.Name} существует. удилите и повторите запуск.");
                    return;
                };
                dir.Create();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ошибка при создании директории ...\"{e.Message}\"");
            }
            
        }
    }
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
