using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // Este código solo anima las imagenes del panel de "cuarta pregunta"
    public class Question4UIScreen : UIScreen
    {
        [Header("Screen")]
        public GameObject screen;

        [Header("Game Objects")]
        public GameObject inputFull;
        public GameObject input;
        public GameObject nextButton;
    
        [Header("Tween elements")]
        public RectTransform answerBox;
        public Image answerBoxImage;
    
        [Header("Tween elements - Text Mesh Pro")]
        public TextMeshProUGUI questionText;
        public TMP_InputField inputField;
    
        [Header("Dialogue box")]
        public DialogueEvent questionDialogue;
    
        private Dialogue _questionDialogue;
    
        private CallbackScreen _callback;
    
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;
        
            _questionDialogue = LoadFileJson.LoadDialogue("Question4");
            questionDialogue.SetSentence(_questionDialogue.sentences[0]);
        
            InitValuesAnimation();
        }

        private void InitValuesAnimation()
        {
            questionText.DOFade(0f, 0f);
        
            answerBox.DOScale(0f, 0f);
            answerBoxImage.DOFade(0f, 0f);
        }

        public override void EnterAnimation()
        {
            var duration = .25f;
            var delay = 1f;

            inputField.text = "";
        
            questionText.DOFade(1f, duration).SetDelay(delay);
            answerBoxImage.DOFade(1f, duration).SetDelay(delay);
            answerBox.DOScale(1f, duration).SetDelay(delay).OnComplete(() =>
            {
                questionDialogue.StartDialogue(_questionDialogue, () =>
                {
                    input.SetActive(true);
                    inputFull.SetActive(true);
                    StartCoroutine(WaitToActiveNextButton(2f));
                });
            });
        }
        
        private IEnumerator WaitToActiveNextButton(float time)
        {
            yield return new WaitForSeconds(time);
            nextButton.SetActive(true);
        }

        public void FadeOffAllElements()
        {
            var duration = .25f;
        
            questionText.DOFade(0f, duration);
        
            answerBox.DOScale(0.33f, duration);
            answerBoxImage.DOFade(0f, duration);
            inputFull.SetActive(false);
            screen.SetActive(false);
        }

        public override void ExitAnimation()
        {
            _callback?.Invoke();
        }
    }
}