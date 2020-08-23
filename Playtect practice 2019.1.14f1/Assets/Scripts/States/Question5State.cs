using Controllers;
using StatePattern;

namespace States
{
    public class Question5State : State
    {
        public Question5State(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
        {
            this.UIScreen.Initialization(null);
        }
    
        public override void Enter()
        {
            base.Enter();
            Controller.question4UIScreen.FadeOffAllElements();
            Controller.flame2UIScreen.FadeOffAllElements();
            Controller.question1UIScreen.FadeOffAllElements();
            Controller.flame1UIScreen.FadeOffAllElements();
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