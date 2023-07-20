namespace Quis_Pliz
{
    internal class Screen
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Screen()
        {
            Width = Console.WindowWidth; 
            Height = Console.WindowHeight;
        }
        public static void ShowQuestion(QuizPart questionData)
        {
            Clear();
            Console.WriteLine(questionData.question);
            Console.WriteLine("1 :  " + questionData.answer.A1);
            Console.WriteLine("2 :  " + questionData.answer.A2);
            Console.WriteLine("3 :  " + questionData.answer.A3);
            Console.WriteLine("4 :  " + questionData.answer.A4);
        }

        public static void ShowReport(IEnumerable<Reports> fm)
        {
            Clear();
            foreach (var fmItem in fm)
            {
                Console.WriteLine("Дата игры: " + fmItem.Data + " Время игры: " + fmItem.Time + " сек Очки: " + fmItem.Number);         
            }
        }

        public static void ShowInPosition(string text, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }

        public static void Greetings()
        {
            string text = "сыграем в КВИЗ?(Y - Да, R - рейтинг, N - выход)";
            Console.ForegroundColor = ConsoleColor.Yellow;
            ShowInPosition(text, (Width - text.Length) / 2, Height / 2);
        }

        public static void ShowSuccess()
        {
            Clear();
            ShowInPosition("Верно",0,0);
        }

        public static void ShowNoCorrect()
        {
            Clear();
            ShowInPosition("Не верно",0,0);
        }

        public static void ShowEndTime()
        {
            Clear();
            ShowInPosition("Время на ответ истекло",0,0);
        }

        public static void ShowTime(int time)
        {
            ShowInPosition(time.ToString(), Width / 2, Height / 2);
        }

        public static void Clear()
        {
            Console.Clear();
        }
    }
}
