using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // Este código solo anima las imagenes del panel de bienvenida
    public class WelcomeUIScreen : UIScreen
    {
        [Header("Screen")]
        public GameObject screen;
        
        [Header("Man Animator")]
        public Animator animator;
    
        private readonly int _talk = Animator.StringToHash("Talk");
    
        [Header("Tween RT elements")]
        public RectTransform dialogueMan;
        public RectTransform secondBoxDialogue;
        
        [Header("Tween Image elements")]
        public Image manBackground;
        public Image manImage;
        public Image firstDialogueBox;

        [Header("Tween elements - Back Element")]
        public Image fichaN2;
        public RectTransform logo;

        [Header("Tween elements - Text Mesh Pro")]
        public TextMeshProUGUI firstDialogueText;
        public TextMeshProUGUI secondDialogueText;
    
        [Header("Dialogue Events")]
        public DialogueEvent firstDialogue;
        public DialogueEvent secondDialogue;
    
        private Dialogue _firstDialogue;
        private Dialogue _secondDialogue;
    
        private CallbackScreen _callback;

        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;
            
            SetDialogues();
            InitValuesAnimation();
        }

        private void SetDialogues()
        {
            _firstDialogue = LoadFileJson.LoadDialogue("DialogueMan1");
            _secondDialogue = LoadFileJson.LoadDialogue("DialogueMan2");
        }

        private void InitValuesAnimation()
        {
            // man
            dialogueMan.DOScale(.5f, 0f);
            dialogueMan.DOAnchorPosY(168f, 0f);
            manBackground.DOFade(.8f, 0f);
            manImage.DOFade(.8f, 0f);
            
            // boxes dialogues
            firstDialogueBox.DOFade(0f, 0f);
            firstDialogueText.DOFade(0f, 0f);
            secondBoxDialogue.DOAnchorPosY(-231f, 0f);
            secondBoxDialogue.DOScale(0f , 0f);
            secondDialogueText.DOFade(0f, 0f);

            // back elements
            fichaN2.DOFade(0f, 0f);
            logo.DOScale(0f, 0f);
            logo.DORotate(new Vector3(0f, 0f, 16f), 0);
            logo.DOAnchorPos(new Vector2(437f, -259f), 0f);
        }
    
        public override void EnterAnimation()
        {
            var duration = .25f;
            dialogueMan.DOScale(1f, duration).SetDelay(.25f);
            dialogueMan.DOAnchorPosY(0, duration).SetDelay(.25f).OnComplete(() =>
            {
                EnterDialogueManInScene(duration);
            });
        }

        private void EnterDialogueManInScene(float duration)
        {
            manBackground.DOFade(1f, duration);
            manImage.DOFade(1f, duration);
            firstDialogueText.DOFade(1f, duration);
            firstDialogueBox.DOFade(1f, duration).OnComplete(StartFirstDialogue);
        }

        private void StartFirstDialogue()
        {
            animator.SetBool(_talk, true);
            firstDialogue.StartDialogue(_firstDialogue, EndFirstDialogue);
        }

        private void EndFirstDialogue()
        {
            var duration = .25f;
            
            animator.SetBool(_talk, false);
            firstDialogueBox.gameObject.SetActive(false);
        
            ExitDialogueManInScene(duration);
        }

        private void ExitDialogueManInScene(float duration)
        {
            manBackground.DOFade(.8f, duration);
            manImage.DOFade(.8f, duration);
            dialogueMan.DOScale(.7f, duration);
            dialogueMan.DOAnchorPosY(109f, duration).OnComplete(() =>
            {
                manBackground.DOFade(.4f, duration);
                manImage.DOFade(.4f, duration);
                dialogueMan.DOScale(.3f, duration);
                dialogueMan.DOAnchorPos(new Vector2(424f, 198f), duration).OnComplete(EnterSecondDialogue);
            });
        }

        private void EnterSecondDialogue()
        {
            var duration = .25f;
        
            fichaN2.DOFade(1f, duration * 2);
            manBackground.DOFade(1f, duration * 2f);
            manImage.DOFade(1f, duration * 2f);
            EnterLogoInScene(duration);
        }

        private void EnterLogoInScene(float duration)
        {
            logo.DORotate(new Vector3(0, 0, -12.28f), duration).OnComplete(() =>
            {
                logo.DORotate(new Vector3(0, 0, 0), duration);
            });
            
            logo.DOScale(.47f, 0f).OnComplete(() => { logo.DOScale(1, duration * 2f); });
            logo.DOAnchorPos(new Vector2(-431f, 350f), duration * 2).OnComplete(StartSecondDialogue);
        }

        private void StartSecondDialogue()
        {
            var duration = .25f;
            
            secondBoxDialogue.DOScale(.6f , 0f).OnComplete(() =>
            {
                secondDialogueText.DOFade(1f, duration);
                secondBoxDialogue.DOAnchorPosY(0f, duration);
                secondBoxDialogue.DOScale(1f , duration).OnComplete(() =>
                {
                    animator.SetBool(_talk, true);
                    secondDialogue.StartDialogue(_secondDialogue, ExitAnimation);    
                });
            });
        
        }

        public override void ExitAnimation()
        {
            animator.SetBool(_talk, false);
        
            var duration = .25f;
        
            // exit dialogue
            secondDialogueText.DOFade(0f, duration);
            secondBoxDialogue.DOScale(.6f, duration);
            secondBoxDialogue.DOAnchorPosY(450f, duration);
            secondBoxDialogue.DOScale(.6f, duration);
            
            // exit man
            manBackground.DOFade(.4f, duration);
            manImage.DOFade(.4f, duration);
            dialogueMan.DOScale(.19f, duration);
            dialogueMan.DOAnchorPosY(450f, duration).OnComplete(() =>
            {
                _callback?.Invoke();
                screen.SetActive(false);
            });
        }
    }
}