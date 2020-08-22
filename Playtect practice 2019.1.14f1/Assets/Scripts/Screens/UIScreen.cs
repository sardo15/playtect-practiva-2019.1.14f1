using UnityEngine;

public delegate void CallbackScreen();

public abstract class UIScreen : MonoBehaviour
{
    public abstract void Initialization(CallbackScreen callback);
    public abstract void EnterAnimation();
    public abstract void ExitAnimation();
}