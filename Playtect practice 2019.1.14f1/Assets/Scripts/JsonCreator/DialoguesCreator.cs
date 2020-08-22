using System.IO;
using UnityEngine;

public class DialoguesCreator : MonoBehaviour
{
    public Narrator narrator;
    
    public void SaveToJson()
    {
        var json = JsonUtility.ToJson(narrator);
        File.WriteAllText(Application.dataPath + "/Json/dialoguesFile.json", json);
    }
}