namespace Quis_Pliz
{
    internal class Game
    {
        Player _player;
        ReturnedStructure[] date;
        int Num;
        Reports report;
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
            Console.WriteLine(date);
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
                ReturnedStructure swap = date[i];
                date[i] = date[r];
                date[r] = swap;
            }
        }

        private void Introduce()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            string message = "сыграем в КВИЗ.(Y - Да, R - рейтинг, N - выход)";
            int width = Console.WindowWidth;
            int heigth = Console.WindowHeight;
            int x = (width - message.Length) / 2;
            int y = heigth / 2;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(message);
        }

        private void Menu()
        {
            ConsoleKeyInfo cki;

            do
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Y)
                {
                    break;
                }
                else if (cki.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    var orderedNumbers = report.GetData().OrderByDescending(n => n.Number).Take(4);
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
                Console.Clear();
                Console.WriteLine(date[i].Question);
                Console.WriteLine("1 :  " + date[i].Answer[0]);
                Console.WriteLine("2 :  " + date[i].Answer[1]);
                Console.WriteLine("3 :  " + date[i].Answer[2]);
                Console.WriteLine("4 :  " + date[i].Answer[3]);
                initial = 0;
                cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                _ = RepeatActionEvery(() => DisplayTime(ref initial), TimeSpan.FromSeconds(1), cancellation.Token);
                buttonKey = Reader.ReadLine(30000);
                Console.Clear();
                cancellation.Cancel();
                Console.SetCursorPosition(0, 0);
                if (buttonKey == date[i].Correct)
                {
                    _player.Point += 1;
                    Console.WriteLine("Верно");
                }
                else if  (buttonKey == "")
                {
                    Console.WriteLine("Время на ответ истекло");
                }
                else
                {
                    _player.Point -= 1;
                    Console.WriteLine("Не верно");
                }
                Thread.Sleep(2000);
            }
            _player.Date = DateTime.Now;
        }

        private void DisplayTime(ref int time)
        {
            Console.SetCursorPosition(Console.WindowWidth/2, Console.WindowHeight/2);
            Console.Write(time++);
            _player.GameTime += 1;
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
            Console.Clear();
            Console.WriteLine("Дата игры: " + _player.Date + " Время игры: " + _player.GameTime + " сек Очки: " + _player.Point);
            Console.ReadKey();
        }
    }


    internal class Player
    {
        public int Point { get; set; }
        public int GameTime { get; set; }
        public DateTime Date  { get; set; }     
    }
}
