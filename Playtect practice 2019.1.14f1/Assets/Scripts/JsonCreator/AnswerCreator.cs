using System.IO;
using UnityEngine;

[System.Serializable]
public class Answer
{
    public string key;
    public string answer;
    public bool isMedal;
    public bool isCorrect;
}

[System.Serializable]
public class AnswerGroup
{
    public Answer[] answers;
}

public class AnswerCreator : MonoBehaviour
{
    public AnswerGroup answerGroup;

    private void Awake()
    {
        LoadFileJson.ResetAnswerResult();
    }

    public void SaveToJson()
    {
        var json = JsonUtility.ToJson(answerGroup);
        File.WriteAllText(Application.dataPath + "/Json/answerFile.json", json);
    }
}
