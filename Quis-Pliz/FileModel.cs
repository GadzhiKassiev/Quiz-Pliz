using System.Text.Json.Serialization;

namespace Quis_Pliz
{
    internal class FileModel
    {
        [JsonPropertyName("time")]
        public TimeSpan Time { get; set; }

        [JsonPropertyName("numberCorrectAnswer")]
        public int Number { get; set; }

        [JsonPropertyName("data")]
        public DateTime Data { get; set; }
    }
}
