using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //получаем путь к рабочему столу юзверя, запустившего программу
        string machineName = Environment.MachineName; // получаем имя машины
        
        string[] discSearch = Environment.GetLogicalDrives(); // список дисков
        Console.Title = "GeoScanner";
        Console.WriteLine("Текущая локальная машина - {0}", machineName);
        Console.WriteLine("Место сохранения отчётов - {0}", savePath);
        Console.WriteLine("Поиск по расширениям \n Вектор: shp, shx, dbf, prj, cbn, xml, MIF, MID, tab, kml, kmz, gps, map \n Растр: tif, tiff, jpg, jpeg, geotiff");

        Console.WriteLine("Обнаруженны следующие логические диски:");
        for (int i = 0; i < discSearch.Length; i++)
        {
            Console.WriteLine(discSearch[i]);
        }
        Console.WriteLine("Укажите минимальный рамзер файлов, байт:");
        int fileSize = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Для начала поиска нажмите Enter");
        Console.ReadLine();
        Console.WriteLine("Start");

        for (int i = 0; i < discSearch.Length; i++) // перебираем диски, ищем по дискам методом Search, лог создаётся для каждого диска отдельно
        {
            
            string searchLoc = discSearch[i]; //
            char disc = searchLoc[0]; // получаем символ диска для имени лога
            string resultLogName = machineName + "_" + disc + ".csv"; // собираем  имя лога
            string fullLogPath = savePath + "\\" + resultLogName; // получаем полное имя файла лога
            Search(searchLoc, fullLogPath, machineName, fileSize);
            //Console.WriteLine("При наличии результата, он записан в файл -  {0} \n Расположение - {1}", resultLogName, savePath); если писать нечего, файл не создаётся
        }
        Console.ReadLine();

    }
    static void Search(string docPath, string fullLogPath, string pcName, int fileSize)
        {

        DirectoryInfo diTop = new DirectoryInfo(docPath);

        try
        {
          

            foreach (var di in diTop.EnumerateDirectories("*"))
            {
                try
                {
                    foreach (var fi in di.EnumerateFiles("*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            string extension = Path.GetExtension(fi.FullName);//получаем расширение файла для сранения
                            if(CheckExtension(extension) == true)
                           // Перебираем все нужные расширения методом опроса в лоб %)
                            
                            {
                                if (fi.Length > fileSize) // отсеиваем мелюзгу для ускорения тестов
                                {
                                    Console.WriteLine($"{fi.FullName}\t{fi.Length}\t{fi.Name}\t{fi.CreationTime}\t{fi.LastAccessTime}\t{fi.LastWriteTime}");

                                    string text = $"\"{pcName}\";\"{fi.FullName}\";\"{fi.Length}\";\"{fi.CreationTime}\";\"{fi.LastWriteTime}\";\"{fi.LastAccessTime}\";\n";
                                    using (FileStream fstream = new FileStream(fullLogPath, FileMode.Append)) // файл лога, создаём если нету, перезаписываем если есть
                                    {
                                        // преобразуем строку в байты
                                        byte[] array = System.Text.Encoding.Default.GetBytes(text);
                                        // запись массива байтов в файл
                                        fstream.Write(array, 0, array.Length);
                                        Console.WriteLine("Файл занесён в лог");
                                    }
                                }

                            }
                        }
                        catch (UnauthorizedAccessException unAuthFile)
                        {
                            Console.WriteLine($"unAuthFile: {unAuthFile.Message}");
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                catch (UnauthorizedAccessException unAuthSubDir)
                {
                    Console.WriteLine($"unAuthSubDir: {unAuthSubDir.Message}");
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        catch (DirectoryNotFoundException dirNotFound)
        {
            Console.WriteLine($"{dirNotFound.Message}");
        }
        catch (UnauthorizedAccessException unAuthDir)
        {
            Console.WriteLine($"unAuthDir: {unAuthDir.Message}");
        }
        catch (PathTooLongException longPath)
        {
            Console.WriteLine($"{longPath.Message}");
        }
       

        
    }
    
    static bool CheckExtension(string extension) //перебираем как можем расширения
    {
        if((extension == ".jpg") || (extension == ".jpeg") ||
           (extension == ".tiff") || (extension == ".geotiff") ||
           (extension == ".tif") || (extension == ".shp") ||
           (extension == ".shx") || (extension == ".dbf") ||
           (extension == ".prj") || (extension == ".cbn") ||
           (extension == ".xml") || (extension == ".MIF") ||
           (extension == ".MID") || (extension == ".tab") ||
           (extension == ".kml") || (extension == ".kmz") ||
           (extension == ".gps") || (extension == ".map"))
        { 
            return true;
        }
        else
        {
            return false;
        }
}
}

   
