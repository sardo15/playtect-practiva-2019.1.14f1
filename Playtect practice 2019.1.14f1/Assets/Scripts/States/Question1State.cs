using Controllers;
using StatePattern;
using States;

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
        UIScreen.gameObject.SetActive(true);
        UIScreen.EnterAnimation();
    }

    public override void Exit()
    {
        base.Exit();
    } 
}