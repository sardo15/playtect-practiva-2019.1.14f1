using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class Question9UIScreen : UIScreen
    {
        [Header("Screen")]
        public GameObject screen;
        
        [Header("Game Objects")]
        public GameObject inputFull;
        public GameObject nextButton;

        public Image fichaN2;
        public Image rectangle;
        
        [Header("Animator Question")]
        public Animator animator;
        
        [Header("Dialogue box")]
        public DialogueEvent attentionDialogue;

        public TMP_InputField inputField;
        
        private Dialogue _attentionDialogue;
        
        private CallbackScreen _callback;
        
        private readonly int _exitQuestion9 = Animator.StringToHash("ExitQuestion9");
        
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;
        }

        public override void EnterAnimation()
        {
            inputField.text = "";
            StartCoroutine(WaitForActivateGoButton(1f));
        }

        private IEnumerator WaitForActivateGoButton(float time)
        {
            yield return new WaitForSeconds(time);
            inputFull.SetActive(true);
            yield return new WaitForSeconds(time);
            nextButton.SetActive(true);
        }

        public override void ExitAnimation()
        {
            _callback?.Invoke();
            fichaN2.DOFade(0f, 0.25f).SetDelay(1f);
            rectangle.DOFade(0f, 0.25f).SetDelay(1f);
            inputField.text = "";
            animator.SetTrigger(_exitQuestion9);
        }
    }
}