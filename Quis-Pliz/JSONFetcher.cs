using System.Text.Json;

namespace Quis_Pliz
{
    internal class JSONFetcher : IFetcher
    {
        QuizBox _quizBox { get; set; }

        public JSONFetcher(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                _quizBox = JsonSerializer.Deserialize<QuizBox>(json);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
                     
        }

        public QuizPart[] fetchDate()
        {
            return _quizBox.quiz;
        }
    }
}
