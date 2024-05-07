using System;
using System.Collections;
using SabberStoneBasicAI.Nodes;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCore.Tasks.PlayerTasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using SabberStoneCore.Tasks;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance = null;
    private Game game;
    private IScore scoring;
    private PowerInterpreter _power;
    private WaitForSeconds _wait;
    private WaitForSeconds _buffer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            scoring = new MidRangeScore();
            _power = FindObjectOfType<PowerInterpreter>();
            _wait = new WaitForSeconds(1.0f);
            _buffer = new WaitForSeconds(0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init(Game game)
    {
        this.game = game;
        StartCoroutine(UpdateRoutine());
    }

    private void OnDestroy()
    {
        game.MainCleanUp();
    }


    private IEnumerator UpdateRoutine()
    {
        List<PlayerTask> tasks = new List<PlayerTask>();
        List<OptionNode> solutions;
        
        while (true)
        {
            yield return _buffer;
            if (game.State == State.RUNNING && game.CurrentPlayer == game.Player2)
            {
                tasks.Clear();
                solutions = OptionNode.GetSolutions(game, game.Player2.Id, scoring, 20, 20);
                //  get highest scored solution
                if (solutions.Count > 1)
                {
                    solutions.OrderByDescending(x => x.Score).First().PlayerTasks(ref tasks);

                    foreach (PlayerTask task in tasks)
                    {
                        yield return _wait;
                        _power.GameProcessWrapper(task);

                        if (game.CurrentPlayer.Choice != null)
                            break;
                    }
                }
            }
        }
    }
}