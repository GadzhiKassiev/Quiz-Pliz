namespace Quis_Pliz
{
    internal class Game
    {
        Player _player;
        QuizPart[] date;
        int Num;
        Reports report;
        const int reportCount = 4;
        DateTime timebeginGame;

        public Game(IFetcher fetcher , int numbers)
        {
            date = fetcher.fetchDate();
            Num = numbers;
            _player = new Player();
        }

        public void Initializer(string path)
        {
            if (File.Exists(path))
            {
                report = new Reports(path);
            }
            else
            {
                File.Create(path);
                report = new Reports(path);
            }
        }

        public void Run()
        {
            Shuffle();
            Num = Num > date.Length ? date.Length : Num;
            date = date[0..Num];
            Introduce();
            Menu();
            Play();
        }

        private void Shuffle()
        {
            Random rnd = new Random();

            for (int i = 0; i < date.Length; i++)
            {
                int r = rnd.Next(0, i+1);
                QuizPart swap = date[i];
                date[i] = date[r];
                date[r] = swap;
            }
        }

        private void Introduce()
        {
            string text = "сыграем в КВИЗ?(Y - Да, R - рейтинг, N - выход)";
            int x = (Screen.Width - text.Length) / 2;
            int y = Screen.Height / 2;
            Screen.DisplayInPosition(() => Screen.Clear(),text, x, y);
        }

        private void Menu()
        {
            ConsoleKeyInfo cki;

            do
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Y)
                {
                    timebeginGame = DateTime.Now;
                    break;
                }
                else if (cki.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    var orderedNumbers = report.GetData().OrderByDescending(n => n.Number).Take(reportCount);
                    foreach (var number in orderedNumbers)
                    {
                        Console.WriteLine("Очки: " + number.Number + " Время игры: " + number.Time + " секунд Дата игры: " + number.Data);
                    }
                }
                else if (cki.Key == ConsoleKey.N)
                {
                    Environment.Exit(0);
                }
            } while (true);
        }

        private void Play()
        {
            string buttonKey;
            int initial;
            CancellationTokenSource cancellation;
            for (int i = 0; i < date.Length; i++)
            {               
                Screen.Display(() => Screen.Clear(), date[i].question, "1 :  " + date[i].answer.A1, "2 :  " + date[i].answer.A2, "3 :  " + date[i].answer.A3, "4 :  " + date[i].answer.A4);
                initial = 0;
                cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                _ = RepeatActionEvery(() => DisplayTime(ref initial), TimeSpan.FromSeconds(1), cancellation.Token);
                buttonKey = Reader.ReadLine(30000);
                cancellation.Cancel();              
                if (buttonKey == date[i].correct)
                {
                    _player.Point += 1;
                    Screen.DisplayInPosition(() => Screen.Clear(),"Верно", 0, 0);
                }
                else if  (buttonKey == "")
                {
                    Screen.DisplayInPosition(() => Screen.Clear(),"Время на ответ истекло", 0, 0);
                }
                else
                {
                    _player.Point -= 1;
                    Screen.DisplayInPosition(() => Screen.Clear(),"Не верно", 0, 0);
                }
                Thread.Sleep(2000);
            }
            _player.Date = DateTime.Now;
            _player.GameTime = _player.Date - timebeginGame;
        }

        private void DisplayTime(ref int time)
        {
            Screen.DisplayInPosition(null, time++.ToString(), Console.WindowWidth / 2, Console.WindowHeight / 2);
        }

        public static async Task RepeatActionEvery(Action action,
          TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                action();
                Task task = Task.Delay(interval, cancellationToken);

                try
                {
                    await task;
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }

        public void End()
        {
            FileModel fm = new FileModel();
            fm.Data = _player.Date;
            fm.Time = _player.GameTime;
            fm.Number = _player.Point;
            report.Load(fm);
            Screen.Display(() => Console.Clear(), "Дата игры: " + _player.Date + " Время игры: " + _player.GameTime + " сек Очки: " + _player.Point);
            Console.ReadLine();
        }
    }


    internal class Player
    {
        public int Point { get; set; }
        public TimeSpan GameTime { get; set; }
        public DateTime Date  { get; set; }     
    }
}
