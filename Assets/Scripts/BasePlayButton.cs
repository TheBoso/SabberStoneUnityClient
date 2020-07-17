using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayButton : MonoBehaviour
{
    public PowerInterpreter powerInterp;

    public virtual void Play()
    {
        if (powerInterp == null)
        {
            powerInterp = GameObject.Find("PowerInterpreter").GetComponent<PowerInterpreter>();
        }
    }
}
