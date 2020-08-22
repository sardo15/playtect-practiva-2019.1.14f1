using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // Este código solo anima las imagenes del panel de "segunda pregunta"
    public class Question2UIScreen : UIScreen
    {
        [Header("Game Objects")]
        public GameObject flame;
        public GameObject input;
        public GameObject inputFull;
        public GameObject nextButton;
    
        [Header("Tween elements")]
        public RectTransform answerBox;
        public Image answerBoxImage;

        [Header("Tween elements - Box Hat")]
        public Image labelRedHat;
        public Image boxRedHat;
        public Image labelYellowHat;
        public Image boxYellowHat;

        [Header("Tween elements - Text Mesh Pro")]
        public TextMeshProUGUI attentionText;
        public TextMeshProUGUI questionText;
    
        public TextMeshProUGUI boxRedHatText;
        public TextMeshProUGUI boxYellowHatText;
    
        [Header("Dialogue box")]
        public DialogueEvent attentionDialogue;
        public DialogueEvent questionDialogue;
    
        private Dialogue _attentionDialogue;
        private Dialogue _questionDialogue;
    
        private CallbackScreen _callback;
    
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;
        
            SetDialogues();
            InitValuesAnimation();
        }

        private void SetDialogues()
        {
            _attentionDialogue = LoadFileJson.LoadDialogue("Attention2");
            _questionDialogue = LoadFileJson.LoadDialogue("Question2");
        }

        private void InitValuesAnimation()
        {
            flame.SetActive(true);
        
            attentionText.DOFade(0f, 0f);
            questionText.DOFade(0f, 0f);
        
            answerBox.DOScale(0.33f, 0f);
            answerBoxImage.DOFade(0f, 0f);
            
            boxRedHat.DOFade(0f, 0f);
            labelRedHat.DOFade(0f, 0f);
            boxYellowHat.DOFade(0f, 0f);
            labelYellowHat.DOFade(0f, 0f);
        
            boxRedHatText.DOFade(0f, 0f);
            boxYellowHatText.DOFade(0f, 0f);

            inputFull.SetActive(false);
        }

        public override void EnterAnimation()
        {
            var duration = .25f;
            var delay = 1f;
            
            attentionDialogue.SetSentence(_attentionDialogue.sentences[0]);
            questionDialogue.SetSentence(_questionDialogue.sentences[0]);
        
            attentionText.DOFade(1f, duration).SetDelay(delay);
            questionText.DOFade(1f, duration).SetDelay(delay);

            answerBox.DOScale(1f, duration).SetDelay(delay).OnComplete(() =>
            {
                attentionDialogue.StartDialogue(_attentionDialogue, () =>
                {
                    questionDialogue.StartDialogue(_questionDialogue, () =>
                    {
                        input.SetActive(true);
                        inputFull.SetActive(true);
                        StartCoroutine(WaitToActiveNextButton(3f));
                    });
                });
            });
        
            answerBoxImage.DOFade(1f, duration).SetDelay(delay);
            boxRedHat.DOFade(1f, duration).SetDelay(delay);
            labelRedHat.DOFade(1f, duration).SetDelay(delay);
            boxYellowHat.DOFade(1f, duration).SetDelay(delay);
            labelYellowHat.DOFade(1f, duration).SetDelay(delay);
        
            boxRedHatText.DOFade(1f, duration).SetDelay(delay);
            boxYellowHatText.DOFade(1f, duration).SetDelay(delay);
        }
        
        private IEnumerator WaitToActiveNextButton(float time)
        {
            yield return new WaitForSeconds(time);
            nextButton.SetActive(true);
        }

        public override void ExitAnimation()
        {
            _callback?.Invoke();
        
            var duration = .15f;
        
            answerBoxImage.DOFade(0f, duration);
            questionText.DOFade(0f, duration).OnComplete(() =>
            {
                inputFull.SetActive(false);
                answerBox.DOScale(0f, 0f);
            });
        }
    }
}