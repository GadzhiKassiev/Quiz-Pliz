using System.Text.Json;

namespace Quis_Pliz
{
    internal class JSONFetcher : IFetcher
    {
        public ReturnedStructure[] returnedArray { get; set; }
        public JSONFetcher(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                var quizBox = JsonSerializer.Deserialize<QuizBox>(json);
                returnedArray = new ReturnedStructure[quizBox!.quiz.Length];
                for (int i = 0; i < quizBox.quiz.Length; i++)
                {
                    returnedArray[i] = new ReturnedStructure(quizBox.quiz[i]);
                }  
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
                     
        }

        public ReturnedStructure[] fetchDate()
        {
            return returnedArray;
        }
    }

    internal class ReturnedStructure
    {
        public string Question { get; set; }
        public string[] Answer { get; set; }
        public string Correct { get; set; }

        public ReturnedStructure(QuizPart part)
        {
            Question = part.question;
            Answer = new string[4];
            Answer[0] = part.answer.A1;
            Answer[1] = part.answer.A2;
            Answer[2] = part.answer.A3;
            Answer[3] = part.answer.A4;
            Correct = part.correct;
        }
    }
}
