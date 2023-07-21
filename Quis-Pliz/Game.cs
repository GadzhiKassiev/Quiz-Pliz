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
        ReportManager _report;
        DateTime _timeBeginGame;
        #endregion

        #region ctor
        public Game(int numbers)
        {
            _quationNumber = numbers;
            _player = new Player();
        }
        #endregion

        public void Initializer(IFetcher fetcher, string path)
        {
            _dataGame = fetcher.fetchDate();
            _report = new ReportManager(path);
        }

        public void Run()
        {
            Shuffle();
            DataRestrict();
            Introduce();
            ChooseOption();
            Play();
        }

        public void End()
        {
            Reports fm = new Reports();
            fm.Data = _player.Date;
            fm.Time = _player.GameTime;
            fm.Number = _player.Point;
            _report.Load(fm);
            Screen.ShowReport(new List<Reports>() {fm});
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

        private void DataRestrict()
        {
            _quationNumber = _quationNumber > _dataGame.Length ? _dataGame.Length : _quationNumber;
            _dataGame = _dataGame[0.._quationNumber];
        }

        private void Introduce()
        {
            Screen.Greetings();
        }

        private void ChooseOption()
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
                    Screen.ShowReport(_report.SelectData(reportCount));
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
            GameTimer timer = new GameTimer();
            for (int i = 0; i < _dataGame.Length; i++)
            {               
                Screen.ShowQuestion(_dataGame[i]);
                timer.Start();
                buttonKey = Reader.ReadLine(timeQuastion * 1000);
                timer.Stop();
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
    }
}
