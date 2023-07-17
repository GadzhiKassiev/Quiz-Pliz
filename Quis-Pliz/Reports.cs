using System.Text.Json;

namespace Quis_Pliz
{
    internal class Reports
    {

        string filepath;

        public Reports(string path)
        {
            filepath = path;
        }

        public void Load(FileModel date)
        {
            string json;
            List<FileModel> data = GetData();               
            data.Add(date);
            var options = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filepath, json);
        }

        public List<FileModel> GetData()
        {
            List<FileModel> data = new List<FileModel>();

            if (new FileInfo(filepath).Length != 0)
            {
                var json = File.ReadAllText(filepath);
                data = JsonSerializer.Deserialize<List<FileModel>>(json)!;                     
            }

            return data!;
        }
    }
}
