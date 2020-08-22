using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // Este código solo anima las imagenes del panel de "que vamos a aprender"
    public class HowGoingToLearnUIScreen : UIScreen
    {
        [Header("screen")]
        public GameObject screen;
        
        [Header("Tween elements")]
        public RectTransform signBoard;
        public Image singBoardImage;
    
        [Header("Tween elements - Box Hat")]
        public Image labelRedHat;
        public Image boxRedHat;
        public Image labelYellowHat;
        public Image boxYellowHat;
    
        [Header("Tween elements - Text Mesh Pro")]
        public TextMeshProUGUI howGoingLearnText;
        public TextMeshProUGUI attentionText;
        public TextMeshProUGUI statementText;
    
        public TextMeshProUGUI boxRedHatText;
        public TextMeshProUGUI boxYellowHatText;

        [Header("Dialogue box")]
        public DialogueEvent howGoingLearnDialogue;
        public DialogueEvent attentionDialogue;
        public DialogueEvent statementDialogue;
    
        [Header("Flame")]
        public RectTransform flame;
        public Image backgroundFlame;
        public Image iconFlame;

        private Dialogue _howGoingLearnDialogue;
        private Dialogue _attentionDialogue;
        private Dialogue _statementDialogue;
    
        private CallbackScreen _callback;
        
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;
        
            SetDialogues();
            InitValuesAnimation();
        }

        private void SetDialogues()
        {
            _howGoingLearnDialogue = LoadFileJson.LoadDialogue("HowGoingLearn");
            _attentionDialogue = LoadFileJson.LoadDialogue("Attention");
            _statementDialogue = LoadFileJson.LoadDialogue("Statement");
        }

        private void InitValuesAnimation()
        {
            howGoingLearnText.DOFade(0f, 0f);
            attentionText.DOFade(0f, 0f);
            statementText.DOFade(0f, 0f);

            signBoard.DOAnchorPosY(-500f, 0f);
            singBoardImage.DOFade(0f, 0f);

            boxRedHat.DOFade(0f, 0f);
            labelRedHat.DOFade(0f, 0f);
            boxYellowHat.DOFade(0f, 0f);
            labelYellowHat.DOFade(0f, 0f);

            boxRedHatText.DOFade(0f, 0f);
            boxYellowHatText.DOFade(0f, 0f);

            flame.DOAnchorPosX(750f, 0f);
            backgroundFlame.DOFade(0f, 0f);
            iconFlame.DOFade(0f, 0f);
        }

        public override void EnterAnimation()
        {
            screen.SetActive(true);
            
            var duration = .25f;
            
            howGoingLearnDialogue.SetSentence(_howGoingLearnDialogue.sentences[0]);
            attentionDialogue.SetSentence(_attentionDialogue.sentences[0]);
            statementDialogue.SetSentence(_statementDialogue.sentences[0]);

            attentionText.DOFade(1f, duration);
            howGoingLearnText.DOFade(1f, duration).OnComplete(() =>
            {
                howGoingLearnDialogue.StartDialogue(_howGoingLearnDialogue, () =>
                {
                    attentionDialogue.StartDialogue(_attentionDialogue, () =>
                    {
                        statementDialogue.StartDialogue(_statementDialogue, ExitAnimation);
                        boxRedHat.DOFade(1f, duration * 2).SetDelay(.5f);
                        labelRedHat.DOFade(1f, duration * 2).SetDelay(.5f);
                        boxRedHatText.DOFade(1f, duration * 2).SetDelay(.5f);
                        boxYellowHat.DOFade(1f, duration * 2).SetDelay(1f);
                        labelYellowHat.DOFade(1f, duration * 2).SetDelay(1f);
                        boxYellowHatText.DOFade(1f, duration * 2).SetDelay(1f);
                    });
                });
            });
        
            signBoard.DOAnchorPosY(42f, duration).SetDelay(.5f);
            singBoardImage.DOFade(1f, duration).SetDelay(.5f);
            statementText.DOFade(1f, duration).SetDelay(.5f);
            flame.DOAnchorPosX(504, duration).SetDelay(.5f);
            backgroundFlame.DOFade(1f, duration).SetDelay(.5f);
            iconFlame.DOFade(1f, duration).SetDelay(.5f);
        }

        public void FadeOffAllElements()
        {
            var duration = .25f;
        
            attentionText.DOFade(0f, duration);
            statementText.DOFade(0f, duration);
        
            singBoardImage.DOFade(0f, duration);

            boxRedHat.DOFade(0f, duration);
            labelRedHat.DOFade(0f, duration);
            boxYellowHat.DOFade(0f, duration);
            labelYellowHat.DOFade(0f, duration);

            boxRedHatText.DOFade(0f, duration);
            boxYellowHatText.DOFade(0f, duration);
        }

        public override void ExitAnimation()
        {
            _callback?.Invoke();
        }
    }
}