using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
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
            //Выбираем корень текущего диска    ###получаем переменную Windows с адресом текущего пользователя
            string PatchProfile = catalog; //Environment.GetEnvironmentVariable("USERPROFILE");
            //ищем все вложенные папки 
            string[] S = SearchDirectory(PatchProfile);
            //создаем строку в которой соберем все пути
            string ListPatch = "найденные папки \n"; //заголовок для строк
            foreach (string folderPatch in S)
            {
                if (HasWritePermissionOnDir(folderPatch) == true)
                {
                    int tempChar = folderPatch.IndexOf(temp);
                    if (tempChar == -1)
                    {
                        //добавляем новую строку в список
                        ListPatch += folderPatch + "\n";
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
                    } else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
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
        
            //флаг SearchOption.AllDirectories означает искать во всех вложенных папках
            string[] ReultSearch = Directory.GetFiles(patch, pattern, SearchOption.AllDirectories);
                //возвращаем список найденных файлов соответствующих условию поиска 
                return ReultSearch;
           

        }

        public static bool HasWritePermissionOnDir(string path)
        {
            var writeAllow = false;
            var writeDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true,
                                        typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                    continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    writeDeny = true;
            }

            return writeAllow && !writeDeny;
        }
        


    }
}

