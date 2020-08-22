using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // Este código solo anima las imagenes del panel de bienvenida
    public class WhatGoingToLearnUIScreen : UIScreen
    {
        [Header("screen")]
        public GameObject screen;
        
        [Header("Woman Animator")]
        public Animator animator;
        
        private readonly int _talk = Animator.StringToHash("Talk");

        [Header("Tween elements")]
        public RectTransform maskAnswerDialogue;
        public RectTransform dialogueWoman;
        public Image background;
        public Image woman;

        [Header("Tween elements - Back Element")]
        public RectTransform labelRectangle;
        public Image imageLabelRectangle;

        [Header("Game Objects")]
        public GameObject parentAnswerDialogue;
        public GameObject cloud;

        [Header("Tween elements - Text Mesh Pro")]
        public TextMeshProUGUI questionText;
        public TextMeshProUGUI answerText;
        
        [Header("Dialogue box")]
        public DialogueEvent questionDialogue;
        public DialogueEvent answerDialogue;
        
        private CallbackScreen _callback;

        private Dialogue _question;
        private Dialogue _answer;
        
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;
            
            SetDialogues();
            InitValueAnimation();
        }

        private void SetDialogues()
        {
            _question = LoadFileJson.LoadDialogue("WhatGoingToLearn");
            _answer = LoadFileJson.LoadDialogue("ToLearn");
        }

        private void InitValueAnimation()
        {
            // woman
            dialogueWoman.DOScale(.6f, 0f);
            dialogueWoman.DOAnchorPosY(450f, 0f);
            background.DOFade(.1f, 0f);
            woman.DOFade(.1f, 0f);

            // back elements
            labelRectangle.DOAnchorPosY(450f, 0f);
            imageLabelRectangle.DOFade(.1f, 0);

            // texts
            questionText.DOFade(0f, 0f);
            answerText.DOFade(0f, 0f);
            
            // Game Objects
            cloud.SetActive(false);
            parentAnswerDialogue.SetActive(false);
        }
        
        public override void EnterAnimation()
        {
            screen.SetActive(true);
            
            var duration = .25f;
            
            EnterWomanInScene(duration);
            EnterBackElementInScene(duration);
        }

        private void EnterWomanInScene(float duration)
        {
            dialogueWoman.DOAnchorPosY(-15, duration).SetDelay(.25f);
            dialogueWoman.DOScale(1f, duration).SetDelay(.25f).OnComplete(() =>
            {
                woman.DOFade(1f, duration);
                background.DOFade(1f, duration).OnComplete(() =>
                {
                    parentAnswerDialogue.SetActive(true);
                    maskAnswerDialogue.DOAnchorPosX(-640f, duration * 8f);
                });
            });
        }

        private void EnterBackElementInScene(float duration)
        {
            questionText.DOFade(1f, duration);
            labelRectangle.DOAnchorPosY(247f, duration).SetDelay(.25f);
            imageLabelRectangle.DOFade(1f, duration).SetDelay(.25f).OnComplete(StartQuestionDialogue);
        }

        private void StartQuestionDialogue()
        {
            animator.SetBool(_talk, true);
            questionDialogue.StartDialogue(_question, () =>
            {
                animator.SetBool(_talk, false);
                answerText.DOFade(1f, .25f);
                StartAnswerDialogue();
            });
        }

        private void StartAnswerDialogue()
        {
            animator.SetBool(_talk, true);
            cloud.SetActive(true);
            answerDialogue.StartDialogue(_answer, ExitAnimation);
        }
        
        public override void ExitAnimation()
        {
            animator.SetBool(_talk, false);
            questionText.DOFade(1f, .5f);
            _callback?.Invoke();
        }
        
        public void WomanExitInScene()
        {
            var duration = .25f;
            
            cloud.SetActive(false);
            parentAnswerDialogue.SetActive(false);
            dialogueWoman.DOAnchorPosX(0, duration).SetDelay(.25f);
            dialogueWoman.DOScale(1.23f, duration).SetDelay(.25f).OnComplete(() =>
            {
                background.DOFade(0f, duration * 2f);
                woman.DOFade(0f, duration * 2f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });
        }
    }
}
