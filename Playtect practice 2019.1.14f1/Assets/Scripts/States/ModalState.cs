using Controllers;
using StatePattern;
using States;

public class ModalState : State
{
    public ModalState(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        Controller.nextButton.gameObject.SetActive(false);
        UIScreen.gameObject.SetActive(true);
        UIScreen.EnterAnimation();
    }

    public override void Exit()
    {
        base.Exit();
        UIScreen.ExitAnimation();
    } 
}