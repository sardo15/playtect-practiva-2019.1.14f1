using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnswerCreator))]
public class AnswerCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var answerCreator = (AnswerCreator) target; 

        if (GUILayout.Button("SaveToJson"))
        {
            answerCreator.SaveToJson();
            Debug.Log("Save to json");
        }
    }
}