using Controllers;
using StatePattern;
using States;

namespace Screens
{
    public class Flame2State : State
    {
        public Flame2State(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
        {
            this.UIScreen.Initialization(Exit);
        }
    
        public override void Enter()
        {
            base.Enter();
            
            UIScreen.EnterAnimation();
        }

        public override void Exit()
        {
            base.Exit();
            Controller.nextButton.gameObject.SetActive(true);
        }
    }
}