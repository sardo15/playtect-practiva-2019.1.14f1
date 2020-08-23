using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Screens
{
    public class Question8UIScreen : UIScreen
    {
        [Header("Screen")]
        public GameObject screen;
        
        [Header("Game Objects")]
        public GameObject inputFull;
        public GameObject nextButton;
        
        [Header("Animator Question")]
        public Animator animator;

        public TextMeshProUGUI questText;
        
        [Header("Dialogue box")]
        public DialogueEvent attentionDialogue;
        
        private Dialogue _attentionDialogue;
        
        private CallbackScreen _callback;
        
        private readonly int _exitQuestion8 = Animator.StringToHash("ExitQuestion8");
        
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;

            SetDialogues();
        }
        
        private void SetDialogues()
        {
            _attentionDialogue = LoadFileJson.LoadDialogue("Attention5");
        }

        public override void EnterAnimation()
        {
            screen.SetActive(true);
            StartCoroutine(WaitForEnterDialogue(1f));
        }
        
        private IEnumerator WaitForEnterDialogue(float time)
        {
            yield return new WaitForSeconds(time);
            attentionDialogue.StartDialogue(_attentionDialogue);
            yield return new WaitForSeconds(time);
            inputFull.SetActive(true);
            yield return new WaitForSeconds(time);
            nextButton.SetActive(true);
        }

        public override void ExitAnimation()
        {
            _callback?.Invoke();
            questText.DOFade(0f, .25f);
            animator.SetTrigger(_exitQuestion8);
        }
    }
}