namespace TransactionalAPIMaddiApp.Helpers.File
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileHelper(IWebHostEnvironment webHost)
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
        public string GetPath()
        {
            return Path.Combine(_webHostEnvironment.ContentRootPath, "AssetsImage");
        }
        public string GetFileBase64(string imageName)
        {
            var filePath = Path.Combine(GetPath(), imageName);

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }
}
