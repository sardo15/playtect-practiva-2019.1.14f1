using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string key;
    [TextArea(3, 20)]
    public string[] sentences;
}

[System.Serializable]
public class Narrator
{
    public Dialogue[] dialogues;
}

public class DialogueEvent : MonoBehaviour
{
    [SerializeField] private float cadenceForLetter = .05f;
    [SerializeField] private float cadenceForSequence = .25f;
    public Color colorFade;
    
    public TextMeshProUGUI sequenceText;
    
    
    private Queue<string> _sentences;
    private CallbackScreen _callback;
    
    private void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, CallbackScreen callback = null)
    {
        _callback = callback;
        
        _sentences.Clear();
        foreach (var sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var sentence = _sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    public void SetSentence(string sentence)
    {
        sequenceText.text = sentence;
    }

    public void EmptyTextField()
    {
        sequenceText.text = "";
    }

    private IEnumerator TypeSentence(string sentence)
    {
        sequenceText.text = sentence;
        var newLetter = "";
        var color = ColorHex.GetStringFromColor(colorFade);
        
        foreach (var letter in sentence.ToCharArray())
        {
            newLetter += letter;
            sequenceText.text = "<#" + color +">" + newLetter + "</color>";
            sentence = sentence.Remove(0, 1);
            sequenceText.text += sentence;
            yield return new WaitForSeconds(cadenceForLetter);
        }

        yield return new WaitForSeconds(cadenceForSequence);
        DisplayNextSentence();
    }

    private void EndDialogue()
    {
        StopAllCoroutines();
        _callback?.Invoke();
    }
}