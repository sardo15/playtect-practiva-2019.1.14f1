using Controllers;
using StatePattern;

namespace States
{
    public class Question8State : State
    {
        public Question8State(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
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