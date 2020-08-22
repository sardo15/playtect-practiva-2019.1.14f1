using Controllers;
using StatePattern;
using States;

public class CongratulationModalState : State
{
    public CongratulationModalState(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
    {
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
        Controller.nextButton.gameObject.SetActive(true);
    }
}