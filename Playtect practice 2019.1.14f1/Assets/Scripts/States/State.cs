using Controllers;
using StatePattern;

namespace States
{
    public abstract class State
    {
        protected readonly GameController Controller;
        protected readonly StateMachine StateMachine;
        protected readonly UIScreen UIScreen;
    
        protected State(GameController controller, StateMachine stateMachine, UIScreen uiScreen)
        {
            this.Controller = controller;
            this.StateMachine = stateMachine;
            this.UIScreen = uiScreen;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }
    }
}