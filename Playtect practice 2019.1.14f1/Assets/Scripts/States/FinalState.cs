using Controllers;
using StatePattern;

namespace States
{
    public class FinalState : State
    {
        public FinalState(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
        {
            this.UIScreen.Initialization(null);
        }
        
        public override void Enter()
        {
            base.Enter();
            Controller.nextButton.gameObject.SetActive(false);
            UIScreen.EnterAnimation();
        }

        public override void Exit()
        {
            base.Exit();
            UIScreen.ExitAnimation();
        } 
    }
}