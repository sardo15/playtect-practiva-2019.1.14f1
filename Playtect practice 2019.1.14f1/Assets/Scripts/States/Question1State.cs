using Controllers;
using StatePattern;

namespace States
{
    public class Question1State : State
    {
        public Question1State(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
        {
            this.UIScreen.Initialization(Exit);
        }
    
        public override void Enter()
        {
            base.Enter();
            Controller.howGoingToLearnUIScreen.FadeOffAllElements();
            Controller.nextButton.gameObject.SetActive(false);
            UIScreen.EnterAnimation();
        }

        public override void Exit()
        {
            base.Exit();
            Controller.nextButton.gameObject.SetActive(true);
        } 
    }
}