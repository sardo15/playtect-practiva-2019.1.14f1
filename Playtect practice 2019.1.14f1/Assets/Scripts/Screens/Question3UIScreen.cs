using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // Este código solo anima las imagenes del panel de "tercera pregunta"
    public class Question3UIScreen : UIScreen
    {
        [Header("Game Objects")]
        public GameObject flame;
        public GameObject input;
        public GameObject inputFull;
        public GameObject nextButton;
    
        [Header("Tween elements")]
        public RectTransform answerBox;
        public Image answerBoxImage;
    
        [Header("Tween elements - Text Mesh Pro")]
        public TextMeshProUGUI questionText;
        public TMP_InputField answerText;
    
        [Header("Dialogue box")]
        public DialogueEvent questionDialogue;
    
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
            _questionDialogue = LoadFileJson.LoadDialogue("Question3");
        }

        private void InitValuesAnimation()
        {
            questionText.DOFade(0f, 0f);
        
            answerBox.DOScale(0f, 0f);
            answerBoxImage.DOFade(0f, 0f);
        
            inputFull.SetActive(false);
        }

        public override void EnterAnimation()
        {
            var duration = .25f;
            var delay = 1f;

            questionDialogue.SetSentence(_questionDialogue.sentences[0]);
            answerText.text = "";

            answerBoxImage.DOFade(1f, duration).SetDelay(delay);
            questionText.DOFade(1f, duration).SetDelay(delay);
            answerBox.DOScale(1f, duration).SetDelay(delay).OnComplete(() =>
            {
                questionDialogue.StartDialogue(_questionDialogue, () =>
                {
                    input.SetActive(true);
                    inputFull.SetActive(false);
                    StartCoroutine(WaitToActiveNextButton(3f));
                });
            
            });
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
                answerBox.DOScale(0f, 0f);
            });
        }
    }
}