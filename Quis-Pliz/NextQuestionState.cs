namespace Quis_Pliz
{
    internal class NextQuestionState: State
    {
        public NextQuestionState(GameStateMachine gsm)
        {
            gameStateMachine = gsm;
        }
        public override void handle()
        {
            GameTimer = new GameTimer();
            QuizPart = GetDate().Pop();       
            Screen.ShowQuestion(QuizPart);
            GameTimer.Start();
            string buttonKey = Reader.ReadLine(timeQuestion * 1000);
            if (buttonKey == QuizPart.correct)
            {
                GameTimer.Stop();
                GetPlayer().Point += 1;
                Screen.ShowSuccess();
            }
            else if (buttonKey == "")
            {
                GameTimer.Stop();
                Screen.ShowEndTime();
            }
            else if (!buttonPattern.Contains(buttonKey))
            {
                Screen.ShowNoCorrectButton();
                gameStateMachine.setState(gameStateMachine.getRepeadQuestionState(GameTimer, QuizPart));
                return;
            }
            else
            {
                GameTimer.Stop();
                GetPlayer().Point -= 1;
                Screen.ShowNoCorrect();
            }
            Thread.Sleep(2000);
            if (GetDate().Count <= 0)
            {
                gameStateMachine.setState(gameStateMachine.getNoQuestionState());
            }
            else
            {
                gameStateMachine.setState(gameStateMachine.getNextQuestionState());
            }
        }
    }
}
