namespace Quis_Pliz
{
    internal class Game
    {
        #region Const
        const int reportCount = 4;
        const int timeQuastion = 30;
        #endregion

        #region Fields
        Player _player;
        QuizPart[] _dataGame;
        int _quationNumber;
        Reports _report;
        DateTime _timeBeginGame;
        #endregion

        #region ctor
        public Game(IFetcher fetcher, int numbers)
        {
            _dataGame = fetcher.fetchDate();
            _quationNumber = numbers;
            _player = new Player();
        }
        #endregion


        public void Initializer(string path)
        {
            if (File.Exists(path))
            {
                _report = new Reports(path);
            }
            else
            {
                File.Create(path);
                _report = new Reports(path);
            }
        }

        public void Run()
        {
            Shuffle();
            _quationNumber = _quationNumber > _dataGame.Length ? _dataGame.Length : _quationNumber;
            _dataGame = _dataGame[0.._quationNumber];
            Introduce();
            Menu();
            Play();
        }

        public void End()
        {
            FileModel fm = new FileModel();
            fm.Data = _player.Date;
            fm.Time = _player.GameTime;
            fm.Number = _player.Point;
            _report.Load(fm);
            Screen.ShowReport(() => Screen.Clear(), fm);
        }

        private void Shuffle()
        {
            Random rnd = new Random();

            for (int i = 0; i < _dataGame.Length; i++)
            {
                int r = rnd.Next(0, i+1);
                QuizPart swap = _dataGame[i];
                _dataGame[i] = _dataGame[r];
                _dataGame[r] = swap;
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
                    _timeBeginGame = DateTime.Now;
                    break;
                }
                else if (cki.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    var orderedNumbers = _report.GetData().OrderByDescending(n => n.Number).Take(reportCount);
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
            for (int i = 0; i < _dataGame.Length; i++)
            {               
                Screen.ShowQuestion(() => Screen.Clear(), _dataGame[i].question, "1 :  " + _dataGame[i].answer.A1, "2 :  " + _dataGame[i].answer.A2, 
                    "3 :  " + _dataGame[i].answer.A3, "4 :  " + _dataGame[i].answer.A4);
                initial = 0;
                cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(timeQuastion));
                _ = RepeatActionEveryAsync(() => DisplayTime(ref initial), TimeSpan.FromSeconds(1), cancellation.Token);
                buttonKey = Reader.ReadLine(timeQuastion * 1000);
                cancellation.Cancel();              
                if (buttonKey == _dataGame[i].correct)
                {
                    _player.Point += 1;
                    Screen.ShowSuccess();
                }
                else if  (buttonKey == "")
                {
                    Screen.ShowEndTime();
                }
                else
                {
                    _player.Point -= 1;
                    Screen.ShowNoCorrect();
                }
                Thread.Sleep(2000);
            }
            _player.Date = DateTime.Now;
            _player.GameTime = _player.Date - _timeBeginGame;
        }

        private void DisplayTime(ref int time)
        {
            Screen.ShowTime(time++);
        }

        private static async Task RepeatActionEveryAsync(Action action, TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                action();
                Task task = Task.Delay(interval, cancellationToken);        
                await task;
                if (task.IsCanceled) return; 
            }
        }
    }
}
