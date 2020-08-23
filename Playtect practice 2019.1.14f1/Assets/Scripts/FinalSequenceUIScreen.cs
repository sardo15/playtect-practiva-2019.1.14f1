using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalSequenceUIScreen : UIScreen
{
    [Header("Game object")]
    public GameObject cupScreen;
    public GameObject afterBtn;
    public GameObject retryBtn;
    public GameObject menuBtn;

    [Header("Tweening object")]
    public RectTransform flameWalkRT;
    public Image flameWalk;
    public Image flameTalk;
    public Image background;
    public Image board;

    public TextMeshProUGUI title;
    public TextMeshProUGUI body;
    public TextMeshProUGUI footer;
    
    [Header("Screen")]
    public GameObject screen;
    
    [Header("Game Objects")]
    public GameObject nextButton;
    
    [Header("Dialogue box")]
    public DialogueEvent titleDialogue;
    public DialogueEvent bodyDialogue;
    public DialogueEvent footerDialogue;
    
    private Dialogue _titleDialogue;
    private Dialogue _bodyDialogue;
    private Dialogue _footerDialogue;
    
    private Dialogue _completeMedalDialogue;
    private Dialogue _incompleteMedalDialogue1;
    private Dialogue _incompleteMedalDialogue2;
    
    private CallbackScreen _callback;

    private bool _hasTheCup;
    public override void Initialization(CallbackScreen callback)
    {
        _callback = callback;
        
        SetDialogues();
        InitValuesAnimation();
    }
    
    private void SetDialogues()
    {
        _titleDialogue = LoadFileJson.LoadDialogue("TitleFinal");
        _bodyDialogue = LoadFileJson.LoadDialogue("BodyBoard");
        _footerDialogue = LoadFileJson.LoadDialogue("Esfuerzate");
        
        _completeMedalDialogue = LoadFileJson.LoadDialogue("CompleteMedal");
        _incompleteMedalDialogue1 = LoadFileJson.LoadDialogue("IncompleteMedal1");
        _incompleteMedalDialogue2 = LoadFileJson.LoadDialogue("IncompleteMedal2");
    }

    private void InitValuesAnimation()
    {
        background.DOFade(0f, 0f);
        flameWalkRT.DOAnchorPosX(-856f, 0f);
        flameWalk.DOFade(0f, 0f);
        flameTalk.gameObject.SetActive(false);
        board.DOFade(0f, 0f);
        
        title.DOFade(0f, 0f);
        body.DOFade(0f, 0f);
        footer.DOFade(0f, 0f);
        
        afterBtn.SetActive(false);
        retryBtn.SetActive(false);
        menuBtn.SetActive(false);
    }

    public override void EnterAnimation()
    {
        screen.SetActive(true);

        var duration = .5f;
        var delay = 3.5f;

        background.DOFade(1f, duration).SetDelay(delay);
        flameWalk.DOFade(1f, duration).SetDelay(delay);
        flameWalkRT.DOAnchorPosX(-400f, duration).SetDelay(delay).OnComplete(() =>
        {
            flameWalkRT.gameObject.SetActive(false);
            flameTalk.gameObject.SetActive(true);
            board.DOFade(1f, duration);
            SetSentences();
        });
    }

    private void SetSentences()
    {
        var numberCorrectAnswers = LoadFileJson.NumberOfCorrectAnswers();
        var totalNumberAnswers = LoadFileJson.TotalNumberOfAnswers();

        if (numberCorrectAnswers == totalNumberAnswers)
        {
            _bodyDialogue = _completeMedalDialogue;
            _hasTheCup = true;
        }
        else
        {
            var missingAnswers = totalNumberAnswers - numberCorrectAnswers;
            _bodyDialogue.sentences[0] = _incompleteMedalDialogue1.sentences[0] + missingAnswers +
                                         _incompleteMedalDialogue2.sentences[0];
        }
        
        titleDialogue.SetSentence(_titleDialogue.sentences[0]);
        bodyDialogue.SetSentence(_bodyDialogue.sentences[0]);
        footerDialogue.SetSentence(_footerDialogue.sentences[0]);
        
        var duration = .5f;
        
        title.DOFade(1f, duration);
        body.DOFade(1f, duration);
        footer.DOFade(1f, duration).OnComplete(() =>
        {
            titleDialogue.StartDialogue(_titleDialogue, () =>
            {
                bodyDialogue.StartDialogue(_bodyDialogue, () =>
                {
                    footerDialogue.StartDialogue(_footerDialogue, ExitAnimation);
                });
            });
        });
    }

    public void FadeOffAllElements()
    {
        var duration = .5f;
        
        background.DOFade(0f, duration);
        flameWalkRT.DOAnchorPosX(-856f, duration);
        flameWalk.DOFade(0f, duration);
        flameTalk.gameObject.SetActive(false);
        board.DOFade(0f, duration);
        
        title.DOFade(0f, duration);
        body.DOFade(0f, duration);
        footer.DOFade(0f, duration).OnComplete(() =>
        {
            cupScreen.SetActive(true);
        });
    }

    public override void ExitAnimation()
    {
        if (_hasTheCup)
        {
            nextButton.SetActive(true);
        }
        else
        {
            afterBtn.SetActive(true);
            retryBtn.SetActive(true);
            menuBtn.SetActive(true);
        }
    }
}
