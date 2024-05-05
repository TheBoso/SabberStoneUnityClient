using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : SingletonScriptableObject<GameSettings>
{
    [field: SerializeField, Header("Minion Play")]
    public AudioClip MinionPlaySound { get; private set; }

    [field: SerializeField]
    public AudioClip MinionLandBoardSound { get; private set; }

    [field: SerializeField]
    public AudioClip MinionLandBoardMedium { get; private set; }

    [field: SerializeField]
    public AudioClip MinionLandBoardHeavy { get; private set; }

    [field: SerializeField, Header("Minion Mechanics")]
    public AudioClip MinionTaunt { get; private set; }

    [field: SerializeField, Header("Character Damage")]
    public AudioClip CharacterDamageLightSound { get; private set; }

    [field: SerializeField]
    public AudioClip CharacterDamageMediumSound { get; private set; }

    [field: SerializeField]
    public AudioClip CharacterDamageHeavySound { get; private set; }

    [field: SerializeField]
    public AudioClip CharacterDiedSound { get; private set; }

    [field: SerializeField, Header("Hand Cards")]
    public AudioClip DrawCardSound { get; private set; }

    [field: SerializeField]
    public AudioClip AddCardToHandSound { get; private set; }

    [field: SerializeField, Header("Hero Power")]
    public AudioClip HeroPowerFlipOff { get; private set; }

    [field: SerializeField]
    public AudioClip HeroPowerFlipOn { get; private set; }

    [field: SerializeField, Header("Mana")]
    public AudioClip LoseMana { get; private set; }

    [field: SerializeField]
    public AudioClip GainMana { get; private set; }
}
