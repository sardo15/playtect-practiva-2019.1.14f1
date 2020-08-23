using System.Collections;
using UnityEngine;

namespace Screens
{
    // Este código solo ejecuta un triggwe para empezar y terminar la animación de "quinta pregunta"
    public class Question5UIScreen : UIScreen
    {
        [Header("Screen")]
        public GameObject screen;
        
        [Header("Game Objects")]
        public GameObject togglesGroup;
        public GameObject nextButton;
        
        [Header("Animator Question")]
        public Animator animator;
        
        [Header("Dialogue box")]
        public DialogueEvent attentionDialogue;
        
        private Dialogue _attentionDialogue;
        
        private CallbackScreen _callback;
        
        private readonly int _enterQuestion5 = Animator.StringToHash("EnterQuestion5");
        private readonly int _exitQuestion5 = Animator.StringToHash("ExitQuestion5");

        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;

            SetDialogues();
        }

        private void SetDialogues()
        {
            _attentionDialogue = LoadFileJson.LoadDialogue("Attention3");
        }

        public override void EnterAnimation()
        {
            screen.SetActive(true);
            togglesGroup.SetActive(true);
            attentionDialogue.SetSentence(_attentionDialogue.sentences[0]);
            StartCoroutine(WaitEnterAnimation(.25f));
        }

        private IEnumerator WaitEnterAnimation(float time)
        {
            yield return new WaitForSeconds(time);
            attentionDialogue.StartDialogue(_attentionDialogue);
            animator.SetTrigger(_enterQuestion5);
        }

        public override void ExitAnimation()
        {
            _callback?.Invoke();
            animator.SetTrigger(_exitQuestion5);
        }
    }
}