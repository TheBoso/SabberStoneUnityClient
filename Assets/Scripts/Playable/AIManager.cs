using System;
using SabberStoneBasicAI.Nodes;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCore.Tasks.PlayerTasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance = null;
    private Game game;
    private IScore scoring;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            scoring = new MidRangeScore();

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init(Game game)
    {
        this.game = game;
    }


    public void PlayTurn()
    {
        if (game != null && game.State == State.RUNNING && game.CurrentPlayer == game.Player2)
        {
            List<OptionNode> solutions = OptionNode.GetSolutions(game, game.Player2.Id, scoring, 40, 40);
            var solution = new List<PlayerTask>();
            solutions.OrderByDescending(p => p.Score).First().PlayerTasks(ref solution);
            foreach (PlayerTask task in solution)
            {

                Debug.Log(task.FullPrint());
                game.Process(task);

                if (game.CurrentPlayer.Choice != null)
                    break;
            }
        }
    }

    private void Update()
    {
        PlayTurn();
    }
}
