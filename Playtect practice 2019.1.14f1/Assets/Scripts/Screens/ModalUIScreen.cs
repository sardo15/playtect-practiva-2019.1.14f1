using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    // Este código solo anima las imagenes del panel de "modal de confirmación de querer avanzar"
    public class ModalUIScreen : UIScreen
    {
        [Header("Tween elements")]
        public Image panelBackground;
        public Image modalBase;

        [Header("Tween elements - Text Mesh Pro")]
        public TextMeshProUGUI titleModal;
    
        private CallbackScreen _callback;
    
        public override void Initialization(CallbackScreen callback)
        {
            _callback = callback;

            InitValuesAnimation();
        }

        private void InitValuesAnimation()
        {
            panelBackground.DOFade(0f, 0f);
            modalBase.DOFade(0f, 0f);
            titleModal.DOFade(0f, 0f);
        }

        public override void EnterAnimation()
        {
            var duration = .75f;
        
            panelBackground.DOFade(.2f, duration);
            modalBase.DOFade(1f, duration);
            titleModal.DOFade(1f, duration);
        }

        public override void ExitAnimation()
        {
            var duration = .75f;
        
            panelBackground.DOFade(0f, duration);
            modalBase.DOFade(0f, duration);
            titleModal.DOFade(0f, duration).OnComplete(() =>
            {
                _callback?.Invoke();
                gameObject.SetActive(false);
            });
        }
    }
}