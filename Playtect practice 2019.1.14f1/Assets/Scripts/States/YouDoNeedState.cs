using Controllers;
using StatePattern;
using States;

public class YouDoNeedState : State
{
    public YouDoNeedState(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
    {
        this.UIScreen.Initialization(Exit);
    }
    
    public override void Enter()
    {
        base.Enter();
        Controller.whatGoingToLearnUIScreen.WomanExitInScene();
        UIScreen.gameObject.SetActive(true);
        UIScreen.EnterAnimation();
    }

    public override void Exit()
    {
        base.Exit();
        Controller.nextButton.gameObject.SetActive(true);
    } 
}