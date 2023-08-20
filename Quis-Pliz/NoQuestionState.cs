using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quis_Pliz
{
    internal class NoQuestionState : State
    {
        GameStateMachine gameStateMachine;
        public NoQuestionState(GameStateMachine gsm)
        {
            gameStateMachine = gsm;
        }
        public override void handle()
        {
            throw new NotImplementedException();
        }
    }
}
