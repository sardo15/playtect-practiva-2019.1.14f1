using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManUIScreen : UIScreen
{
    public DialogueEvent dialogueEvent;
    public Dialogue dialogue;
    public Animator animator;
    
    public RectTransform parent;
    public Image background;
    public Image icon;

    public Image dialogueBox;
    public TextMeshProUGUI textFade;
    
    private CallbackScreen _callback;
    private readonly int _talk = Animator.StringToHash("Talk");
    
    public override void Initialization(CallbackScreen callback)
    {
        _callback = callback;
    }

    public override void EnterAnimation()
    {
        parent.DOScale(1, 0.25f).SetEase(Ease.InFlash).SetDelay(.25f);
        parent.DOAnchorPosY(0f, .25f).SetEase(Ease.Linear).SetDelay(.25f).OnComplete(() =>
        {
            background.DOFade(1f, .25f);
            icon.DOFade(1f, .25f).OnComplete(() =>
            {
                textFade.DOFade(1f, .25f).SetDelay(.25f);
                dialogueBox.DOFade(1f, .25f).SetDelay(.25f).OnComplete(() =>
                {
                    animator.SetBool(_talk, true);
                    dialogueEvent.StartDialogue(dialogue, ExitAnimation);
                });
            });
        });
    }

    public override void ExitAnimation()
    {
        animator.SetBool(_talk, false);
        dialogueBox.DOFade(0f, .25f);
        textFade.DOFade(0f, .25f).SetDelay(.25f);
        background.DOFade(.5f, .25f);
        icon.DOFade(.5f, .25f);
        parent.DOAnchorPosY(150f, .25f);
        parent.DOScale(.4f, .25f).SetEase(Ease.InFlash).SetDelay(.25f).OnComplete(() =>
        {
            parent.DOAnchorPosY(185f, .25f);
            parent.DOAnchorPosX(416f, .25f).OnComplete(() =>
            {
                _callback?.Invoke();
            });
        });
    }
}