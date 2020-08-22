using System;
using Screens;
using StatePattern;
using States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// crear un nuevo scripts que menje la interacción

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("Welcome UI Screen")]
        public WelcomeUIScreen welcomeUIScreen;
        public WhatGoingToLearnUIScreen whatGoingToLearnUIScreen;
        public YouDoNeedUIScreen youDoNeedUIScreen;
        public HowGoingToLearnUIScreen howGoingToLearnUIScreen;

        [Header("Questions UI Screen")]
        public Question1UIScreen question1UIScreen;
        public Question2UIScreen question2UIScreen;
        public Question3UIScreen question3UIScreen;
        public Question4UIScreen question4UIScreen;
        // public Question5UIScreen question5UIScreen;
    
        [Header("Flames UI Screen")]
        public Flame1UIScreen flame1UIScreen;
        public Flame2UIScreen flame2UIScreen;

        [Header("Modals")]
        public ModalUIScreen modalUIScreen;
        public CongratulationModalUIScreen congratulationModalUIScreen;
    
        private StateMachine _sequenceMachine;
    
        [Header("Inputs form")]
        public TMP_InputField inputFieldText;
        public TMP_InputField inputFieldNumber;
    
        public Button nextButton;

        public Button acceptModalButton;
        public Button cancelModalButton;

        [NonSerialized] public WelcomeState welcome;
        [NonSerialized] public WhatGoingToLearnState whatGoingToLearn;
        [NonSerialized] public YouDoNeedState youDoNeedState;
        [NonSerialized] public HowGoingToLearnState howGoingToLearnState;

        [NonSerialized] public Question1State question1State;
        [NonSerialized] public Flame1State flame1State;
        [NonSerialized] public Question2State question2State;
        [NonSerialized] public Question3State question3State;
        [NonSerialized] public Question4State question4State;
        [NonSerialized] public Flame2State flame2State;
        [NonSerialized] public Question5State question5State;

        [NonSerialized] public ModalState modalState;
        [NonSerialized] public CongratulationModalState congratulationModalState;

        private State _afterState;
        private State _beforeState;
    
        private void Start()
        {
            InitButtons();
            SetStateMachine();
            SetFirstSequenceAnimationsStates();
            SetQuestionsStates();
            SetModalsStates();

            _sequenceMachine.Initialize(welcome);
        }

        private void SetQuestionsStates()
        {
            question1State = new Question1State(this, _sequenceMachine, question1UIScreen);
            flame1State = new Flame1State(this, _sequenceMachine, flame1UIScreen);
            question2State = new Question2State(this, _sequenceMachine, question2UIScreen);
            question3State = new Question3State(this, _sequenceMachine, question3UIScreen);
            question4State = new Question4State(this, _sequenceMachine, question4UIScreen);
            flame2State = new Flame2State(this, _sequenceMachine, flame2UIScreen);
            // question5State = new Question5State(this, _sequenceMachine, question5UIScreen);
        }

        private void SetModalsStates()
        {
            modalState = new ModalState(this, _sequenceMachine, modalUIScreen);
            congratulationModalState = new CongratulationModalState(this, _sequenceMachine, congratulationModalUIScreen);
        }

        private void SetFirstSequenceAnimationsStates()
        {
            welcome = new WelcomeState(this, _sequenceMachine, welcomeUIScreen);
            whatGoingToLearn = new WhatGoingToLearnState(this, _sequenceMachine, whatGoingToLearnUIScreen);
            youDoNeedState = new YouDoNeedState(this, _sequenceMachine, youDoNeedUIScreen);
            howGoingToLearnState = new HowGoingToLearnState(this, _sequenceMachine, howGoingToLearnUIScreen);
        }

        private void SetStateMachine()
        {
            _sequenceMachine = new StateMachine();
        }

        private void InitButtons()
        {
            // botón de avanzar
            nextButton.onClick.AddListener(OnNextButtonClick);

            // botones del modal de confimación
            acceptModalButton.onClick.AddListener(OnAcceptModalButtonClick);
            cancelModalButton.onClick.AddListener(OnCancelModalButtonClick);
        }

        private void OnNextButtonClick()
        {
            if (_sequenceMachine.CurrentState == welcome)
            {
            }

            if (_sequenceMachine.CurrentState == whatGoingToLearn)
            {
                _afterState = whatGoingToLearn;
                _sequenceMachine.ChangeState(youDoNeedState);
                nextButton.gameObject.SetActive(false);
                return;
            }

            if (_sequenceMachine.CurrentState == youDoNeedState)
            {
                _afterState = youDoNeedState;
                _sequenceMachine.ChangeState(howGoingToLearnState);
                nextButton.gameObject.SetActive(false);
                return;
            }

            if (_sequenceMachine.CurrentState == howGoingToLearnState)
            {
                _afterState = howGoingToLearnState;
                _sequenceMachine.ChangeState(question1State);
                nextButton.gameObject.SetActive(false);
                return;
            }

            if (_sequenceMachine.CurrentState == question1State)
            {
                if (inputFieldText.text == LoadFileJson.LoadAnswer("question0").answer)
                {
                    LoadFileJson.SaveAnswerResult("question0");
                    Debug.Log(true);
                }
            
                _afterState = question1State;
                _sequenceMachine.ChangeState(flame1State);
                nextButton.gameObject.SetActive(false);
                return;
            }

            if (_sequenceMachine.CurrentState == flame1State)
            {
                _afterState = flame1State;
                _sequenceMachine.ChangeState(question2State);
                nextButton.gameObject.SetActive(false);
                return;
            }
        
            if (_sequenceMachine.CurrentState == question2State)
            {
                if (inputFieldNumber.text == LoadFileJson.LoadAnswer("question1").answer)
                {
                    LoadFileJson.SaveAnswerResult("question1");
                    Debug.Log(inputFieldNumber.text + "" + true);
                }
                Debug.Log(inputFieldNumber.text);
                _afterState = question2State;
                _sequenceMachine.ChangeState(modalState);
                nextButton.gameObject.SetActive(false);
                return;
            }

            if (_sequenceMachine.CurrentState == question3State)
            {
                if (inputFieldNumber.text == LoadFileJson.LoadAnswer("question2").answer)
                {
                    LoadFileJson.SaveAnswerResult("question2");
                    Debug.Log(inputFieldNumber.text + "" + true);
                }
                _afterState = question3State;
                _sequenceMachine.ChangeState(modalState);
                return;
            }

            if (_sequenceMachine.CurrentState == question4State)
            {
                _afterState = question4State;
                _sequenceMachine.ChangeState(flame2State);
                nextButton.gameObject.SetActive(false);
                return;
            }
        
            if (_sequenceMachine.CurrentState == flame2State)
            {
                _afterState = flame2State;
                _sequenceMachine.ChangeState(question2State);
                nextButton.gameObject.SetActive(false);
                return;
            }

            if (_sequenceMachine.CurrentState == question5State)
            {
            
            }

            if (_sequenceMachine.CurrentState == congratulationModalState)
            {
                _sequenceMachine.CurrentState.Exit();
                _afterState.Exit();
                if (_afterState == question2State)
                {
                    _sequenceMachine.ChangeState(question3State);
                }

                if (_afterState == question3State)
                {
                    _sequenceMachine.ChangeState(question4State);
                }
                return;
            }
        }

        private void OnAcceptModalButtonClick()
        {
            _sequenceMachine.CurrentState.Exit();
            _sequenceMachine.ChangeState(congratulationModalState);
        }

        private void OnCancelModalButtonClick()
        {
            _sequenceMachine.CurrentState.Exit();
            _sequenceMachine.ChangeState(_afterState);
        }
    }
}