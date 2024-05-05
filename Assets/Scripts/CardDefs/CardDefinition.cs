using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   We want to be able to store our cards along with important info such as spawn effects, sounds ie
/// </summary>
public class CardDefinition : ScriptableObject
{
    [field: SerializeField] public AudioClip SummonSound { get; set; }
    [field: SerializeField] public AudioClip DeathSound { get;  set; }
    [field: SerializeField] public AudioClip AttackSound { get;  set; }
    [field: SerializeField] public AudioClip SelectSound { get;  set; }


}
