using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGoButton : MonoBehaviour
{
    public GameObject goButton;

    public void ActivateGoButtonToFinishAnimation()
    {
        goButton.SetActive(true);
    }
}
