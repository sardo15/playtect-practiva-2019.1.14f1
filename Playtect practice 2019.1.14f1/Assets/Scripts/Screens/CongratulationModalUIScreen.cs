using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class CongratulationModalUIScreen : UIScreen
    {
        [Header("Screen")]
        public GameObject screen;
        
        [Header("Tween elements")]
        public Image panelBackground;
        public Image flag;
        public Image medal;
    
        [Header("Tween elements - Text Mesh Pro")]
        public TextMeshProUGUI titleModal;
        public TextMeshProUGUI textModal;

        public GameObject nextButton;
    
        private CallbackScreen _callback;
    
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;
        
            InitValuesAnimation();
        }

        private void InitValuesAnimation()
        {
            panelBackground.DOFade(0f, 0f);
            flag.DOFade(0f, 0f);
            medal.DOFade(0f, 0f);
            titleModal.DOFade(0f, 0f);
            textModal.DOFade(0f, 0f);
        }

        public override void EnterAnimation()
        {
            var duration = .5f;
            
            screen.SetActive(true);
        
            panelBackground.DOFade(.2f, duration);
            flag.DOFade(1f, duration);
            titleModal.DOFade(1f, duration);
            textModal.DOFade(1f, duration);
            medal.DOFade(1f, duration).OnComplete(() =>
            {
                nextButton.SetActive(true);
            });
        }

        public override void ExitAnimation()
        {
            var duration = .5f;
        
            titleModal.DOFade(0f, duration);
            textModal.DOFade(0f, duration);
            panelBackground.DOFade(0f, duration);
            flag.DOFade(0f, duration);
            medal.DOFade(0f, duration).OnComplete(() =>
            {
                _callback?.Invoke();
                nextButton.SetActive(false);
                screen.SetActive(false);
            });
        }
    }
}