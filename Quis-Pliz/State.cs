namespace Quis_Pliz
{
    internal abstract class State
    {
        protected const int timeQuestion = 30;
        protected string buttonPattern = "1234";
        protected GameStateMachine gameStateMachine;

        public GameTimer GameTimer { get; set; }
        public QuizPart QuizPart { get; set; }

        abstract public void handle();

        protected Player GetPlayer()
        {
            return gameStateMachine.Player();
        }

        protected Stack<QuizPart> GetDate()
        {
            return gameStateMachine.DataGame();
        }
    }
}
