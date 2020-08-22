using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    public List<Toggle> toggles;
    public int value;
    
    public void SetToggleAmount(int value)
    {
        this.value = value;
        
        for (int i = 0; i < toggles.Count; i++)
        {
            if (i <= value)
            {
                toggles[i].isOn = true;
            }
            else
            {
                toggles[i].isOn = false;
            }
        }
    }
}
