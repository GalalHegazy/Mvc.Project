using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Mvc.Project.PL.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFileAsync(IFormFile file ,string FolderName)
        {
                          //(\Users\galal\source\repos\Mvc.Project\Mvc.Project.PL\\wwwroot\\Files\\FolderName)
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",FolderName);

            if(!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);


            //(d5d454d4d4.Png)
            //string FileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string FileName = $"{file.FileName}";

            //(\Users\galal\source\repos\Mvc.Project\Mvc.Project.PL\\wwwroot\\Files\\FolderName\\d5d454d4d4.Png)
            string FilePath = Path.Combine(FolderPath,FileName);


            var FileStream = new FileStream(FilePath,FileMode.Create);
            
           await file.CopyToAsync(FileStream);

            return FileName;    

        }
        public static void DeleteFile(string fileName,string folderName)
        {
            //(\Users\galal\source\repos\Mvc.Project\Mvc.Project.PL\\wwwroot\\Files\\FolderName\\d5d454d4d4.Png)
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName,fileName);

            if (File.Exists(FilePath))
                File.Delete(FilePath);

        }
    }
}
