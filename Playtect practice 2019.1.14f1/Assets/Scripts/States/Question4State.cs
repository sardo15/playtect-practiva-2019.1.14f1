using Controllers;
using StatePattern;
using States;

public class Question4State : State
{
    public Question4State(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
    {
        this.UIScreen.Initialization(null);
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
        UIScreen.ExitAnimation();
        Controller.nextButton.gameObject.SetActive(false);
    }
}