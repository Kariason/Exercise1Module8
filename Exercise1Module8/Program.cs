using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1Module8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до папки");
            string path = Console.ReadLine();
            DelDir(path);
        }

        public static void DelDir(string path)
        {
            int quantityFiles = 0;
            DateTime LastWriteTime = Directory.GetLastAccessTime(path);
            TimeSpan timeSpan = TimeSpan.FromMinutes(30);
            DateTime thresholdTime = DateTime.Now.Subtract(timeSpan);
            DirectoryInfo newDir = new DirectoryInfo(path);
            DirectoryInfo[] folder = newDir.GetDirectories();
            FileInfo[] newFile = newDir.GetFiles();

            if (Directory.Exists(path))
            {
                if (LastWriteTime < thresholdTime)
                {
                    try
                    {
                        foreach (FileInfo f in newFile)
                        {
                            quantityFiles++;
                            f.Delete();
                        }

                        foreach (DirectoryInfo df in folder)
                        {
                            DelDir(df.FullName);
                        }

                        foreach (DirectoryInfo dir in newDir.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                    }
                    Console.WriteLine($"В папке {path} файлов удалено: {quantityFiles}");
                }
                else
                {
                    Console.WriteLine("Ошибка удаления: Последние изменения в папке были менее 30мин назад");
                }
            }
        }
    }
}
