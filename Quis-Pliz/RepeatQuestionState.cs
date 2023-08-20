﻿namespace Quis_Pliz
{
    internal class RepeatQuestionState : State
    {
        public RepeatQuestionState(GameStateMachine gsm)
        {
            gameStateMachine = gsm;
        }
        public override void handle()
        {
            string buttonKey = Reader.ReadLine((timeQuestion - GameTimer.CurrentCount) * 1000);
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
