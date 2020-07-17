using SabberStoneCore.Enchants;
using SabberStoneCore.Model;
using SabberStoneCore.Model.Entities;
using SabberStoneCore.Tasks.PlayerTasks;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class OnCardClick : BasePlayButton
{
    public Transform handzone;

    public override void Play()
    {
        base.Play();



        //IPlayable card = powerInterp._game.CurrentPlayer.HandZone[transform.GetSiblingIndex()];

        // yucky loop through hand till we find it VERY BUGGY

        int c = 0;
        foreach (Transform child in transform.parent)
        {
            Card card = powerInterp._game.CurrentPlayer.HandZone[c].Card;
            IPlayable playableVersion = powerInterp._game.CurrentPlayer.HandZone[c];

            if (child.name == card.Id && playableVersion.IsPlayable)
            {
                // process play task of specific card
                powerInterp._game.Process(PlayCardTask.Any(powerInterp._game.CurrentPlayer, playableVersion));
                powerInterp.UpdateGraphics();
                break;

            }
                    c++;
                    }
    }
}
