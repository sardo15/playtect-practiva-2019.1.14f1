using System;
using Screens;
using StatePattern;
using States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("First Sequence Animations UI Screen")]
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
        
        [Header("Inputs form")]
        public TMP_InputField inputFieldText;
        public TMP_InputField inputFieldNumber;
    
        [Header("Buttons")]
        public Button nextButton;

        [Header("Modals Buttons")]
        public Button acceptModalButton;
        public Button cancelModalButton;

        // First Sequence Animations states
        [NonSerialized] public WelcomeState welcome;
        [NonSerialized] public WhatGoingToLearnState whatGoingToLearn;
        [NonSerialized] public YouDoNeedState youDoNeedState;
        [NonSerialized] public HowGoingToLearnState howGoingToLearnState;

        // Questions states
        [NonSerialized] public Question1State question1State;
        [NonSerialized] public Question2State question2State;
        [NonSerialized] public Question3State question3State;
        [NonSerialized] public Question4State question4State;
        [NonSerialized] public Question5State question5State;
        
        // Flame Massage
        [NonSerialized] public Flame1State flame1State;
        [NonSerialized] public Flame2State flame2State;

        // Modals States
        [NonSerialized] public ModalState modalState;
        [NonSerialized] public CongratulationModalState congratulationModalState;
        
        private StateMachine _sequenceMachine;

        private State _previousState;
        private State _afterState;

        private string _keyAnswers;
        
        private void Start()
        {
            InitButtons();
            
            SetStateMachine();
            SetFirstSequenceAnimationsStates();
            SetQuestionsStates();
            SetFlameMassageStates();
            SetModalsStates();

            _sequenceMachine.Initialize(welcome);
        }
        
        private void SetStateMachine()
        {
            _sequenceMachine = new StateMachine();
        }
        
        private void SetFirstSequenceAnimationsStates()
        {
            welcome = new WelcomeState(this, _sequenceMachine, welcomeUIScreen);
            whatGoingToLearn = new WhatGoingToLearnState(this, _sequenceMachine, whatGoingToLearnUIScreen);
            youDoNeedState = new YouDoNeedState(this, _sequenceMachine, youDoNeedUIScreen);
            howGoingToLearnState = new HowGoingToLearnState(this, _sequenceMachine, howGoingToLearnUIScreen);
        }

        private void SetQuestionsStates()
        {
            question1State = new Question1State(this, _sequenceMachine, question1UIScreen);
            question2State = new Question2State(this, _sequenceMachine, question2UIScreen);
            question3State = new Question3State(this, _sequenceMachine, question3UIScreen);
            question4State = new Question4State(this, _sequenceMachine, question4UIScreen);
            // question5State = new Question5State(this, _sequenceMachine, question5UIScreen);
        }

        private void SetFlameMassageStates()
        {
            flame1State = new Flame1State(this, _sequenceMachine, flame1UIScreen);
            flame2State = new Flame2State(this, _sequenceMachine, flame2UIScreen);
        }

        private void SetModalsStates()
        {
            modalState = new ModalState(this, _sequenceMachine, modalUIScreen);
            congratulationModalState = new CongratulationModalState(this, _sequenceMachine, congratulationModalUIScreen);
        }

        private void StateMachineSequenceActions()
        {
            if (_sequenceMachine.CurrentState == welcome)
            {
            }
            
            if (_sequenceMachine.CurrentState == whatGoingToLearn)
            {
                SavePreviousState(whatGoingToLearn);
                _sequenceMachine.ChangeState(youDoNeedState);
                return;
            }
            
            if (_sequenceMachine.CurrentState == youDoNeedState)
            {
                SavePreviousState(youDoNeedState);
                _sequenceMachine.ChangeState(howGoingToLearnState);
                return;
            }
            
            if (_sequenceMachine.CurrentState == howGoingToLearnState)
            {
                SavePreviousState(howGoingToLearnState);
                _sequenceMachine.ChangeState(question1State);
                return;
            }
            
            if (_sequenceMachine.CurrentState == question1State)
            {
                CompareAnswerToQuestion("question0");
                SavePreviousState(question1State);
                _sequenceMachine.ChangeState(flame1State);
                return;
            }
            
            if (_sequenceMachine.CurrentState == flame1State)
            {
                SavePreviousState(flame1State);
                _sequenceMachine.ChangeState(question2State);
                return;
            }
            
            if (_sequenceMachine.CurrentState == question2State)
            {
                SetKeyAnswers("question1");
                SavePreviousState(question2State);
                _sequenceMachine.ChangeState(modalState);
                return;
            }
            
            if (_sequenceMachine.CurrentState == question3State)
            {
                SetKeyAnswers("question2");
                SavePreviousState(question3State);
                _sequenceMachine.ChangeState(modalState);
                return;
            }
            
            if (_sequenceMachine.CurrentState == question4State)
            {
                SavePreviousState(question4State);
                _sequenceMachine.ChangeState(flame2State);
                return;
            }
            
            if (_sequenceMachine.CurrentState == flame2State)
            {
                SavePreviousState(flame2State);
                _sequenceMachine.ChangeState(question2State);
                return;
            }
            
            if (_sequenceMachine.CurrentState == question5State)
            {
                SavePreviousState(question5State);
                _sequenceMachine.ChangeState(null);
                return;
            }
            
            if (_sequenceMachine.CurrentState == congratulationModalState)
            {
                _sequenceMachine.CurrentState.Exit();
                _previousState.Exit();
                
                AdvanceToNextSequenceState();
            }
        }

        private void AdvanceToNextSequenceState()
        {
            if (_previousState == question2State)
            {
                _sequenceMachine.ChangeState(question3State);
                return;
            }

            if (_previousState == question3State)
            {
                _sequenceMachine.ChangeState(question4State);
                return;
            }

            if (_previousState == question4State)
            {
                return;
            }
        }

        private void SavePreviousState(State previousState)
        {
            _previousState = previousState;
        }
        
        private void CompareAnswerToQuestion(string key)
        {
            if (inputFieldText.text == LoadFileJson.LoadAnswer(key).answer)
            {
                LoadFileJson.SaveAnswerResult(key);
            }
        }

        private void SetKeyAnswers(string key)
        {
            _keyAnswers = key;
        }
        
        private void InitButtons()
        {
            nextButton.onClick.AddListener(OnNextButtonClick);
            
            acceptModalButton.onClick.AddListener(OnAcceptModalButtonClick);
            cancelModalButton.onClick.AddListener(OnCancelModalButtonClick);
        }

        private void OnNextButtonClick()
        {
            StateMachineSequenceActions();
        }

        private void OnAcceptModalButtonClick()
        {
            _sequenceMachine.CurrentState.Exit();
            CompareAnswerToQuestionForMedal(_keyAnswers);
            
            if (LoadFileJson.IsCorrectAnswerResult(_keyAnswers))
            {
                _sequenceMachine.ChangeState(congratulationModalState);
            }
            else
            {
                AdvanceToNextSequenceState();
            }
        }

        private void OnCancelModalButtonClick()
        {
            _sequenceMachine.CurrentState.Exit();
            _sequenceMachine.ChangeState(_previousState);
        }
        
        private void CompareAnswerToQuestionForMedal(string key)
        {
            if (inputFieldNumber.text == LoadFileJson.LoadAnswer(key).answer)
            {
                LoadFileJson.SaveAnswerResult(key);
            }
        }
    }
}