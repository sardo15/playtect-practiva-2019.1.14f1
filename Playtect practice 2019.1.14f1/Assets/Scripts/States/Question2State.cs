using Controllers;
using StatePattern;
using States;

public class Question2State : State
{
    public Question2State(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
    {
        this.UIScreen.Initialization(null);
    }
    
    public override void Enter()
    {
        base.Enter();
        Controller.question1UIScreen.FadeOffAllElements();
        Controller.flame1UIScreen.FadeOffAllElements();
        UIScreen.gameObject.SetActive(true);
        UIScreen.EnterAnimation();
    }

    public override void Exit()
    {
        base.Exit();
        UIScreen.ExitAnimation();
    } 
}