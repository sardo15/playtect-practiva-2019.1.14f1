using Controllers;
using StatePattern;
using States;
using UnityEngine;

public class Question3State : State
{
    public Question3State(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller,
        stateMachine, uiScreen)
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
    }
}