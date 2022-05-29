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
            //(string StudentName, DateTime StudentBirth) StB;
            var Groups = new Dictionary<string, List<(string StudentName, DateTime StudentBirth)>>();

            try
            {
                using (var fs = new FileStream($"C:\\Users\\{Environment.UserName}\\Desktop\\Students.dat", FileMode.Open))
                {
                    students = (Student[])formatter.Deserialize(fs);
                };


            }
            catch (Exception e)
            {
                Console.WriteLine($"При чтении файла возникла ошибка: ...\"{e.Message}\"");
            };

            for (int i = 0; i < students.Length; i++)//наполняем словарь списками групп
            {
                if (!Groups.TryAdd(students[i].Group, new List<(string StudentName, DateTime StudentBirth)> { (students[i].Name, students[i].DateOfBirth) }))
                {
                    Groups[students[i].Group].Add((students[i].Name, students[i].DateOfBirth));
                }
            };

            DirectoryInfo dir = new DirectoryInfo($"C:\\Users\\{Environment.UserName}\\Desktop\\Student");
            try
            {
                if (dir.Exists)
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
            foreach (var group in Groups.Keys)
            {
                using (StreamWriter  fs = File.CreateText(string.Concat(dir.FullName, "\\", group)))
                {
                    foreach (var student in Groups[group])
                    {
                        fs.WriteLine("{0}, {1}", student.StudentName, student.StudentBirth);
                    }
                }
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
    //public struct StB
    //{
    //    public string StudentName { get; }
    //    public DateTime StudentBirth { get; }

    //    public StB(String Name, DateTime date)
    //    {
    //        StudentName = Name;
    //        StudentBirth = date;
    //    }
    //}
    
}
