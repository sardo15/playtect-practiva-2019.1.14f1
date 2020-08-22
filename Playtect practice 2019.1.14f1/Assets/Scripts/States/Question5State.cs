using Controllers;
using StatePattern;
using States;

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
        UIScreen.gameObject.SetActive(true);
        UIScreen.EnterAnimation();
    }

    public override void Exit()
    {
        base.Exit();
        UIScreen.ExitAnimation();
    } 
}