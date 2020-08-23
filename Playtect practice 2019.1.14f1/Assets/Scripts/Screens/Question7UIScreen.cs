using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Screens
{
    public class Question7UIScreen : UIScreen
    {
        [Header("Screen")]
        public GameObject screen;
        
        [Header("Game Objects")]
        public GameObject inputFull;
        public GameObject nextButton;
        
        [Header("Animator Question")]
        public Animator animator;
        
        [Header("Dialogue box")]
        public DialogueEvent attentionDialogue;
        public DialogueEvent questionDialogue;

        public TextMeshProUGUI attentionText;

        private Dialogue _attentionDialogue;
        private Dialogue _questionDialogue;
        
        private CallbackScreen _callback;
        
        private readonly int _exitQuestion7 = Animator.StringToHash("ExitQuestion7");
        
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;

            SetDialogues();
        }
        
        private void SetDialogues()
        {
            _attentionDialogue = LoadFileJson.LoadDialogue("Attention4");
            _questionDialogue = LoadFileJson.LoadDialogue("Question5");
        }

        public override void EnterAnimation()
        {
            screen.SetActive(true);
            attentionText.DOFade(1f, .25f);
            StartCoroutine(WaitForEnterDialogue(1f));
        }

        private IEnumerator WaitForEnterDialogue(float time)
        {
            yield return new WaitForSeconds(time);
            attentionDialogue.StartDialogue(_attentionDialogue);
            yield return new WaitForSeconds(time);
            questionDialogue.StartDialogue(_questionDialogue);
            inputFull.SetActive(true);
            yield return new WaitForSeconds(time);
            nextButton.SetActive(true);
        }

        public override void ExitAnimation()
        {
            _callback?.Invoke();
            attentionText.text = "";
            animator.SetTrigger(_exitQuestion7);
        }
    }
}