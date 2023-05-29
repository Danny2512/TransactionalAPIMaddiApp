namespace TransactionalAPIMaddiApp.Helpers.File
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileHelper(IConfiguration configuration, IWebHostEnvironment webHost)
        {
            _webHostEnvironment = webHost;
        }
        public void AddFile(IFormFile file, string fileName, string guid)
        {
            var directoryPath = GetPath();
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, guid + Path.GetExtension(fileName));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(GetPath(), fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        public string GetPath()
        {
            return Path.Combine(_webHostEnvironment.ContentRootPath, "AssetsImage");
        }
        public string GetFileBase64(string path)
        {
            var filePath = Path.Combine(GetPath(), path);

            if (System.IO.File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
            else
            {
                // El archivo no existe, puedes manejar el caso de error de alguna manera
                return null;
            }

        }
    }
}