using SabberStoneCore.Enchants;
using SabberStoneCore.Model;
using SabberStoneCore.Tasks.PlayerTasks;
using SabberStoneCore.Tasks.SimpleTasks;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class OnHeroPowerClick : BasePlayButton
{
  
  public override void Play()
    {

        base.Play();

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
