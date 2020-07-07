using SabberStoneCore.Enchants;
using SabberStoneCore.Model;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneCore.Tasks.SimpleTasks;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class HeroPowerButton : MonoBehaviour
{
    public PowerInterpreter powerInterp;
  public void UseHeroPower()
    {
        if (powerInterp == null)
        {
            powerInterp = GameObject.Find("PowerInterpreter").GetComponent<PowerInterpreter>();
        }

        // For now just try activate it
        if (powerInterp._game.CurrentPlayer.Hero.HeroPower.IsPlayable)
        {
            Debug.Log("Used Hero Power!");
            powerInterp._game.Process(HeroPowerTask.Any(powerInterp._game.CurrentPlayer));
            powerInterp.UpdateGraphics();

        }
        else
        {
            Debug.Log("cannot use");
        }

        

    }
}
