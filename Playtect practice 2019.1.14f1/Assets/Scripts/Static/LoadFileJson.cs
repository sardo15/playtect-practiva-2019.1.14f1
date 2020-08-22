using System.IO;
using States;
using UnityEngine;

public static class LoadFileJson
{
    public static Dialogue LoadDialogue(string key)
    {
        var json = File.ReadAllText(Application.dataPath + "/Json/dialoguesFile.json");
        var narrator = JsonUtility.FromJson<Narrator>(json);
        
        foreach (var dialogue in narrator.dialogues)
        {
            if (dialogue.key == key)
            {
                return dialogue;
            }
        }

        Debug.LogError("No key support " + key);
        return null;
    }
    
    public static Answer LoadAnswer(string key)
    {
        var json = File.ReadAllText(Application.dataPath + "/Json/answerFile.json");
        var answerGroup = JsonUtility.FromJson<AnswerGroup>(json);
        
        foreach (var answer in answerGroup.answers)
        {
            if (answer.key == key)
            {
                return answer;
            }
        }

        Debug.LogError("No key support " + key);
        return null;
    }

    public static void SaveAnswerResult(string key)
    {
        var json = File.ReadAllText(Application.dataPath + "/Json/answerFile.json");
        var answerGroup = JsonUtility.FromJson<AnswerGroup>(json);

        foreach (var answers in answerGroup.answers)
        {
            if (answers.key == key)
            {
                answers.isCorrect = true;
            }
        }
        
        var newJson = JsonUtility.ToJson(answerGroup);
        File.WriteAllText(Application.dataPath + "/Json/answerFile.json", newJson);
    }
}