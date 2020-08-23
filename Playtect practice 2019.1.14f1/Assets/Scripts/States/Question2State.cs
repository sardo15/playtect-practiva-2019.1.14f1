using Controllers;
using StatePattern;

namespace States
{
    public class Question2State : State
    {
        private bool _hasBeenCalled;
        public Question2State(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
        {
            this.UIScreen.Initialization(null);
        }
    
        public override void Enter()
        {
            base.Enter();
            UIScreen.EnterAnimation();
            
            Controller.nextButton.gameObject.SetActive(false);
            
            if (_hasBeenCalled)
            {
                return;
            }

            _hasBeenCalled = true;

            Controller.question1UIScreen.FadeOffAllElements();
            Controller.flame1UIScreen.FadeOffAllElements();
        }

        public override void Exit()
        {
            base.Exit();
            UIScreen.ExitAnimation();
            Controller.nextButton.gameObject.SetActive(false);
        } 
    }
}