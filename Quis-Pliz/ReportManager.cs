using System.Text.Json;

namespace Quis_Pliz
{
    internal class ReportManager
    {

        string filepath;

        public ReportManager(string path)
        {
            filepath = path;
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public void Load(Reports date)
        {
            string json;
            List<Reports> data = GetData();               
            data.Add(date);
            var options = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filepath, json);
        }

        public IEnumerable<Reports> SelectData(int dataNum)
        {
            return GetData().OrderByDescending(n => n.Number).Take(dataNum);
        }

        private List<Reports> GetData()
        {
            List<Reports> data = new List<Reports>();

            if (new FileInfo(filepath).Length != 0)
            {
                var json = File.ReadAllText(filepath);
                data = JsonSerializer.Deserialize<List<Reports>>(json)!;                     
            }

            return data!;
        }
    }
}
