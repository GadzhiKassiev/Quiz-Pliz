namespace Quis_Pliz
{
    internal class FileManager
    {
        public void CreateIfNotExist(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }
    }
}
