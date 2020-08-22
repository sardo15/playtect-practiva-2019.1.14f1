using Controllers;
using StatePattern;
using UnityEngine;

namespace States
{
    public class WhatGoingToLearnState : State
    {
        public WhatGoingToLearnState(GameController controller, StateMachine stateMachine, UIScreen uiScreen) : base(controller, stateMachine, uiScreen)
        {
            this.UIScreen.Initialization(Exit);
        }
    
        public override void Enter()
        {
            base.Enter();
            UIScreen.EnterAnimation();
        }

        public override void Exit()
        {
            base.Exit();
            Controller.nextButton.gameObject.SetActive(true);
        } 
    }
}