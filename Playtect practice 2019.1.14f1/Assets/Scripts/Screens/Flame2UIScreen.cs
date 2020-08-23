using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // Este código solo anima las imagenes del panel de "segunda respuesta de la flamita pregunta"
    public class Flame2UIScreen : UIScreen
    {
        [Header("Screen")]
        public GameObject screen;
        
        [Header("Game Objects")]
        public GameObject flame;
        public GameObject nextButton;

        [Header("Tween elements")]
        public Image panelBackground;
        public Image bigFlameImage;
        public Image bigFlameBackImage;
        public RectTransform bigFlame;
        public RectTransform bigFlameBackground;
    
        [Header("Tween elements - Text Mesh Pro")]
        public TextMeshProUGUI dialogueText;
    
        [Header("Dialogue box")]
        public DialogueEvent dialogue;
    
        private Dialogue _dialogue;
    
        private CallbackScreen _callback;
    
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;
        
            SetDialogues();
            InitValuesAnimation();
        }

        private void SetDialogues()
        {
            _dialogue = LoadFileJson.LoadDialogue("FlameAnswer1");
        }

        private void InitValuesAnimation()
        {
            panelBackground.DOFade(0f, 0f);
            bigFlameBackImage.DOFade(0f, 0f);
            bigFlameBackground.DOAnchorPosY(-750f, 0f);
            bigFlame.DOScale(.6f, 0f);
            bigFlame.DOAnchorPos(new Vector2(504f, 140f), 0f);
            dialogueText.DOFade(0f, 0f);
        }

        public override void EnterAnimation()
        {
            var duration = .25f;
            
            screen.SetActive(true);
            
            dialogue.SetSentence(_dialogue.sentences[0]);
            
            flame.SetActive(false);
            nextButton.SetActive(false);
            bigFlameImage.DOFade(1f, duration);
            panelBackground.DOFade(.3f, duration).OnComplete(() =>
            {
                bigFlame.DOScale(1f, duration);
                bigFlame.DOAnchorPos(new Vector2(198f, 4.25f), duration).OnComplete(() =>
                {
                    dialogueText.DOFade(1f, duration * 2);
                    bigFlameBackImage.DOFade(1f, duration * 2);
                    bigFlameBackground.DOAnchorPosY(21.5f, duration * 2).OnComplete(() =>
                    {
                        dialogue.StartDialogue(_dialogue, ExitAnimation);
                    });
                });
            });
        }
    
        public void FadeOffAllElements()
        {
            var duration = .25f;
        
            panelBackground.DOFade(0f, duration);
            bigFlameBackImage.DOFade(0f, duration);
            bigFlame.DOScale(0f, 0f);
            dialogueText.DOFade(0f, duration);
            bigFlameImage.DOFade(0f, duration).OnComplete(() =>
            {
                screen.SetActive(false);
            });
        }

        public override void ExitAnimation()
        {
            nextButton.SetActive(true);
            _callback?.Invoke();
        }
    }
}