using Controllers;
using StatePattern;

namespace States
{
    public class WelcomeState : State
    {
        public WelcomeState(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
        {
            this.UIScreen.Initialization(Exit);
        }
    
        public override void Enter()
        {
            base.Enter();
            UIScreen.gameObject.SetActive(true);
            UIScreen.EnterAnimation();
        }

        public override void Exit()
        {
            base.Exit();
            StateMachine.ChangeState(Controller.whatGoingToLearn);
        }    
    }
}
