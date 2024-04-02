namespace Demo.PL.Helper
{
    public static class DocumentSetting 
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", FolderName);

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            using var fileStreem = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStreem);

            return fileName;
        }

        public static void DeleteFile(string fileName , string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName, fileName);

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
