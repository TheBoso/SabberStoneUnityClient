using System.Collections;
using System.Collections.Generic;
using SabberStoneCore.Model;
using SabberStoneCore.Tasks.PlayerTasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class HeroPowerButton : Button
{
    private PowerInterpreter _interpreter;
    private Game _game;
    protected override void Awake()
    {
        base.Awake();
        _interpreter = FindObjectOfType<PowerInterpreter>();
        onClick.AddListener(OnClickHeroPower);

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onClick.RemoveListener(OnClickHeroPower);
    }

    private void OnClickHeroPower()
    {
        if (_game == null)
        {
            _game = _interpreter.Game;
        }
        if (_game.CurrentPlayer == _game.Player1 &&
            _game.Player1.Hero.HeroPower.IsPlayable)
        {
            _interpreter.GameProcessWrapper(HeroPowerTask.Any(_game.CurrentPlayer));
        }
    }
}
