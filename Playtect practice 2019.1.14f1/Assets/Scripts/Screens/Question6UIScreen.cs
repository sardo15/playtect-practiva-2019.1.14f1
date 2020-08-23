using System.Collections;
using UnityEngine;

namespace Screens
{
    public class Question6UIScreen : UIScreen
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
        
        private readonly int _exitQuestion6 = Animator.StringToHash("ExitQuestion6");
        
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
            attentionDialogue.StartDialogue(_attentionDialogue);
        }

        public override void ExitAnimation()
        {
            _callback?.Invoke();
            animator.SetTrigger(_exitQuestion6);
        }
    }
}