using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : SingletonScriptableObject<GameSettings>
{
   
   
   [field: SerializeField] public AudioClip MinionPlaySound { get; private set; }
   [field: SerializeField] public AudioClip MinionLandBoardSound { get; private set; }
   [field: SerializeField] public AudioClip MinionLandBoardMedium { get; private set; }
   [field: SerializeField] public AudioClip MinionLandBoardHeavy { get; private set; }
   [field: SerializeField] public AudioClip CharacterDamageLightSound { get; private set; }
   [field: SerializeField] public AudioClip CharacterDamageMediumSound { get; private set; }
   [field: SerializeField] public AudioClip CharacterDamageHeavySound { get; private set; }
   [field: SerializeField] public AudioClip CharacterDiedSound { get; private set; }
   
   [field: SerializeField] public AudioClip DrawCardSound { get; private set; }
   [field: SerializeField] public AudioClip AddCardToHandSound { get; private set; }
   
   [field: SerializeField] public AudioClip HeroPowerFlipOff { get; private set; }
   [field: SerializeField] public AudioClip HeroPowerFlipOn { get; private set; }

   
   
}
