using Controllers;
using StatePattern;
using States;

public class HowGoingToLearnState : State
{
    public HowGoingToLearnState(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
    {
        this.UIScreen.Initialization(Exit);
    }
    
    public override void Enter()
    {
        base.Enter();
        Controller.youDoNeedUIScreen.PuzzleExitTransition();
        UIScreen.gameObject.SetActive(true);
        UIScreen.EnterAnimation();
    }

    public override void Exit()
    {
        base.Exit();
        Controller.nextButton.gameObject.SetActive(true);
    }
}