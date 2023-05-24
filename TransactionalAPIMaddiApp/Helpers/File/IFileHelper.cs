namespace TransactionalAPIMaddiApp.Helpers.File
{
    public interface IFileHelper
    {
        void AddFile(IFormFile file, string fileName, string guid);
        string GetFileBase64(string imageName);
        string GetPath();
    }
}
