using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SabberStoneCore;
using SabberStoneCore.Model;
using SabberStoneCore.Tasks.PlayerTasks;
using UnityEngine.UI;
using SabberStoneBasicAI.Nodes;
using SabberStoneBasicAI.Score;
using System.Linq;
using SabberStoneCore.Enchants;

public class PlayerChoice : MonoBehaviour
{
 

    [SerializeField]
    private Text label;
    private PowerInterpreter power;

    public PlayerTask task;

    public void SetPowerInterpreter(PowerInterpreter power)
    {
        this.power = power;
    }


    public void SetText(string optionString)
    {
        label.text = optionString;
    }

    public void ProcessTask()
    {

        power._game.Process(task);
            power.UpdateGraphics();
        Destroy(gameObject);

    }


    }


