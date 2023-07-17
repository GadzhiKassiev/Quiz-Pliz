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
        public static void ShowQuestion(Action action, params string[] str)
        {
            action();
            for(int i = 0; i < str.Length; i++)
            {
                Console.WriteLine(str[i]);
            }
        }

        public static void ShowReport(Action action, FileModel fm)
        {
            action();       
            Console.WriteLine("Дата игры: " + fm.Data + " Время игры: " + fm.Time + " сек Очки: " + fm.Number);         
        }

        public static void DisplayInPosition(Action action, string text, int x, int y)
        {
            if (action is not null) action();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y);
            if (text is not null) Console.WriteLine(text);
        }

        public static void Clear()
        {
            Console.Clear();
        }
    }
}
