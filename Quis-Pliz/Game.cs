namespace Quis_Pliz
{
    internal class Game
    {
        #region Const
        const int reportCount = 4;
        #endregion

        #region Fields
        GameStateMachine _gameStateMachine;
        public Player Player { get; set; }
        public Stack<QuizPart> DataGame { get; set; }
        int _questionNumber;
        ReportManager _report;
        DateTime _timeBeginGame;
        #endregion

        #region ctor
        public Game(int numbers)
        {
            _questionNumber = numbers;
            Player = new Player();
            _gameStateMachine = new GameStateMachine(this);
        }
        #endregion

        public void Initializer(QuizPart[] data, ReportManager rm)
        {
            QuizPart[] qp = Shuffle(data);
            qp = DataRestrict(qp);
            DataGame = new Stack<QuizPart>(qp);
            _report = rm;
        }

        public void Run()
        {
            Introduce();
            ChooseOption();
            Play();
        }

        public void End()
        {
            Reports fm = new Reports();
            fm.Data = Player.Date;
            fm.Time = Player.GameTime;
            fm.Number = Player.Point;
            _report.Load(fm);
            Screen.ShowReport(new List<Reports>() {fm});
        }

        private QuizPart[] Shuffle(QuizPart[] qp)
        {
            Random rnd = new Random();

            for (int i = 0; i < qp.Length; i++)
            {
                int r = rnd.Next(0, i+1);
                QuizPart swap = qp[i];
                qp[i] = qp[r];
                qp[r] = swap;
            }
            return qp;
        }

        private QuizPart[] DataRestrict(QuizPart[] qp)
        {
            _questionNumber = _questionNumber > qp.Length ? qp.Length : _questionNumber;
            return qp[0.._questionNumber];
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
            _gameStateMachine.Launch();
            Player.Date = DateTime.Now;
            Player.GameTime = Player.Date - _timeBeginGame;
        }
    }
}
