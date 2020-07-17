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
    public Game game;
    public PowerInterpreter powerInterp;
    public IScore scoring;


    private void Start()
    {
        scoring = new MidRangeScore();
        game = powerInterp._game;
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
                powerInterp.UpdateGraphics();

                if (game.CurrentPlayer.Choice != null)
                    break;
            }
        }
        else if (game == null)
        {
            game = powerInterp._game;
        }
    }

    private void Update()
    {
        PlayTurn();
    }
}
