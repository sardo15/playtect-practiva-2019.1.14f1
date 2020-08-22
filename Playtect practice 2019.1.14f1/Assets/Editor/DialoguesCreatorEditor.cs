using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialoguesCreator))]
public class DialoguesCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var dialoguesCreator = (DialoguesCreator) target; 

        if (GUILayout.Button("SaveToJson"))
        {
            dialoguesCreator.SaveToJson();
            Debug.Log("Save to json");
        }
    }
}