using System.Text.Json.Serialization;

namespace Quis_Pliz
{
    internal class QuizBox
    {
        public QuizPart[] quiz { get; set; }

        public QuizBox(QuizPart[] quiz)
        {
            this.quiz = quiz;
        }
    }

    internal class QuizPart
    {
        public string question { get; set;}
        public QuizAnswer answer { get; set; }
        public string correct { get; set; }

      

        public QuizPart(string question, QuizAnswer answer, string correct)
        {
            this.question = question;
            this.answer = answer;
            this.correct = correct;
        }
    }

    internal class QuizAnswer
    {
        [JsonPropertyName("a1")]
        public string A1 { get; set; }

        [JsonPropertyName("a2")]
        public string A2 { get; set; }

        [JsonPropertyName("a3")]
        public string A3 { get; set; }

        [JsonPropertyName("a4")]
        public string A4 { get; set; }


        public QuizAnswer(string a1, string a2, string a3, string a4)
        {
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
        }
    }
}
