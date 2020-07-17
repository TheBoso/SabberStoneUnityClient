using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager:MonoBehaviour
{
    // Start is called before the first frame update
    public static FrameManager instance = null;

    public Sprite[] MinionFrameSprites;
    public Sprite[] SpellFrameSprites;


    private void Start()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
 
        
        }

        else
        {
            Destroy(gameObject);
        }


    }


}
