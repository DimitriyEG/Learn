using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace TestConcole
{
    class Program
    {

        static void Main(string[] args)
        {
            string temp = "$";
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            char curentDisk = startupPath[0];
            string catalog = curentDisk + ":\\";
            //получаем переменную Windows с адресом текущего пользователя
            string PatchProfile = catalog; //Environment.GetEnvironmentVariable("USERPROFILE");
            //ищем все вложенные папки 
            string[] S = SearchDirectory(PatchProfile);
            //создаем строку в которой соберем все пути
            string ListPatch = "найденные папки \n"; //заголовок для строк
            foreach (string folderPatch in S)
            {
                //добавляем новую строку в список
                // ListPatch += folderPatch + "\n";
                try
                {
                    //пытаемся найти данные в папке 
                    string[] F = SearchFile(folderPatch, "*.png");
                    foreach (string FF in F)
                    {
                        
                        //добавляем файл в список 
                        ListPatch += FF + "\n";
                    }
                }
                catch
                {
                }
            }
            MessageBox.Show(ListPatch);
        }
        static string[] SearchDirectory(string patch)
        {
            //находим все папки в по указанному пути
            string[] ReultSearch = Directory.GetDirectories(patch);
            //возвращаем список директорий
            return ReultSearch;
        }
        static string[] SearchFile(string patch, string pattern) 
        {
            /*флаг SearchOption.AllDirectories означает искать во всех вложенных папках*/
            string[] ReultSearch = Directory.GetFiles(patch, pattern, SearchOption.AllDirectories);
            //возвращаем список найденных файлов соответствующих условию поиска 
            return ReultSearch;
        }
        /*{
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            char curentDisk = startupPath[0];
            string userName = Environment.UserName;
            string pcName = Environment.MachineName;
            string catalog = curentDisk + ":\\";
            //string path = @"C:\C\result.txt";
            //string[] fileTypes = new string[18] { "*.jpg", "*.tiff", "*.jpeg", "*.geotiff", "*.tif", "*.shp", "*.shx", "*.dbf", "*.prj", "*.cbn", "*.xml", "*.MIF", "*.MID", "*.tab", "*.kml", "*.kmz", "*.gps", "*.map" };
            string resultName = userName + "_" + pcName + "_" + curentDisk + ".txt";
            string path = startupPath;
            string resultFileName = path + "\\" + resultName;
            string fileData;
            string fileName;
            string filter = "*.*";
            string filePull = System.IO.Directory.GetFiles(@"C:\");

            //проводим поиск в выбранном каталоге и во всех его подкаталогах
            try
            {
                foreach (string findedFile in Directory.EnumerateFiles(catalog, filter, SearchOption.AllDirectories))
                {




                    //FileInfo; // FI; 
                    try
                    {
                        //по полному пути к файлу создаём объект класса FileInfo
                        FileInfo FI = new FileInfo(findedFile);
                        //найденный результат выводим в консоль (имя, путь, размер, дата создания файла)
                        fileName = FI.Name;
                        if ((fileName.EndsWith(".jpg")) || (fileName.EndsWith(".tiff")) || (fileName.EndsWith(".jpeg")) || (fileName.EndsWith(".geotiff")))
                        {
                            fileData = $"File name -  {FI.Name }; Full name - { FI.FullName }; File size - { FI.Length }_byte; Create: { FI.CreationTime}; Path: {FI.DirectoryName}";
                            Console.WriteLine(fileData);
                            writeLine(fileData);

                        }
                        else
                        {
                            continue;
                        }

                    }
                    catch (UnauthorizedAccessException)
                    {
                        //Console.WriteLine("Acces denide");
                        continue;

                    }


                }
            } catch
            {
                
            }

            Console.ReadKey();
            string writeLine(string fileStr)
            {
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    string createText = fileStr + Environment.NewLine;
                    File.WriteAllText(resultFileName, createText, Encoding.UTF8);
                } else
                {
                    string appendText = fileStr + Environment.NewLine;
                    File.AppendAllText(path, appendText);
                }
                return null; //убрать бы тебя
            }
        }*/


    }
}
/*  string wordResponse = fileData;
  StreamReader myfile = File.OpenText(path);
  string line = myfile.ReadLine();
  int position = line.IndexOf(fileData);
  Console.WriteLine(line);
  if (position != true)
  {
      Console.WriteLine(fileData);
      Console.WriteLine(iterate);
      addLine(fileData);
  }
  else
  {
      continue;
  }

*/
