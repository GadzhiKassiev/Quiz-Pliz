namespace Quis_Pliz
{
    internal class GameStateMachine
    {
        Game _game;
        State _nextQuestionState;
        State _repeadQuestionState;
        State _noQuestionState;
        State _state;
        public GameStateMachine(Game g)
        {
            _game = g;
            _nextQuestionState = new NextQuestionState(this);
            _repeadQuestionState = new RepeatQuestionState(this);
            _noQuestionState = new NoQuestionState(this);
            _state = _nextQuestionState;
        }

        public void Launch()
        {
            while(_state != _noQuestionState)
            {
                _state.handle();
            }
        }

        public void setState(State state)
        {
            _state = state;
        }

        public State getNextQuestionState()
        {
            return _nextQuestionState;
        }

        public State getRepeadQuestionState(GameTimer gameTimer, QuizPart quizPart)
        {
            _repeadQuestionState.QuizPart = quizPart;
            _repeadQuestionState.GameTimer = gameTimer;
            return _repeadQuestionState;
        }

        public State getNoQuestionState()
        {
            return _noQuestionState;
        }

        public Player Player()
        {
            return _game.Player;
        }

        public Stack<QuizPart> DataGame()
        {
            return _game.DataGame;
        }
    }
}
