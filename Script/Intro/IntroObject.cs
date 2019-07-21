using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IntroObject : MonoBehaviour {
    protected bool isTouch = false;

    public abstract void SkipIntroScene();
    public abstract void TouchScreen();
}
