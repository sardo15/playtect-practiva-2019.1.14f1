using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // Este código solo anima las imagenes del panel de "que necesitas"
    public class YouDoNeedUIScreen : UIScreen
    {
        [Header("screen")]
        public GameObject screen;
        
        [Header("Dialogue box")]
        public DialogueEvent questionDialogue;

        [Header("Tween elements")]
        public RectTransform bookRT;
        public RectTransform pencilRT;

        [Header("Tween elements - Back Element")]
        public RectTransform finalPuzzleRectTransform;
        public Image finalPuzzleImage;
    
        private Dialogue _question;
    
        private CallbackScreen _callback;
        
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;
        
            SetDialogues();
            InitValueAnimation();
        }

        private void SetDialogues()
        {
            _question = LoadFileJson.LoadDialogue("YouDoNeed");
        }

        private void InitValueAnimation()
        {
            bookRT.DOAnchorPosX(-760f, 0);
            pencilRT.DOAnchorPosX(760f, 0);
            finalPuzzleImage.DOFade(0f, 0f);
        }

        public override void EnterAnimation()
        {
            screen.SetActive(true);
            
            var duration = .25f;
        
            bookRT.DOAnchorPosX(-85f, duration).SetDelay(.5f);
            pencilRT.DOAnchorPosX(85f, duration).SetDelay(.5f).OnComplete(() =>
            {
                finalPuzzleImage.DOFade(1f, duration).OnComplete(() =>
                {
                    bookRT.gameObject.SetActive(false);
                    pencilRT.gameObject.SetActive(false);
                });
            });
        
            questionDialogue.StartDialogue(_question, ExitAnimation);
        }

        public override void ExitAnimation()
        {
            _callback?.Invoke();
        }

        public void PuzzleExitTransition()
        {
            var duration = .5f;

            finalPuzzleImage.DOFade(.5f, duration);
            finalPuzzleRectTransform.DOScale(.32f, duration);
            finalPuzzleRectTransform.DOAnchorPosY(450f, duration).SetDelay(.25f).OnComplete(() =>
            {
                questionDialogue.EmptyTextField();
                gameObject.SetActive(false);
            });
        }
    }
}
