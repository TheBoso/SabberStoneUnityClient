// https://github.com/mtimkovich/hearthsounds/blob/master/hearthsounds.py - sound download
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using UnityEditor;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    private const string SOUND_DIR = "Assets/Resources/CardAudio";
    private const string SOUND_BASE = "https://storage.googleapis.com/hearthsounds/";

    private readonly string[] SOUND_TAGS = new string[]
    {
        "play",
        "stringer",
        "attack",
        "death",
        "trigger",
        "customsummon"
    };

    public enum CardSoundType
    {
        PLAY = 0,
        STRINGER,
        ATTACK,
        DEATH,
        TRIGGER,
        CUSTOMSUMMON
    }
    public static CardHolder instance = null;

    private Dictionary<string, CardDefinition> _allCards;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            _allCards = Resources.LoadAll<CardDefinition>("CardDefs").ToDictionary(KeySelector);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private string KeySelector(CardDefinition arg)
    {
        return arg.name;
    }

    public bool TryGetCardDefinition(string id, out CardDefinition def)
    {
        if (_allCards.TryGetValue(id, out def))
        {
            return true;
        }
        else
        {
            //  we probably need to create the def
            CardDefinition card = ScriptableObject.CreateInstance<CardDefinition>();
            card.name = id;
            AssetDatabase.CreateAsset(card, $"Assets/Resources/CardDefs/{card.name}.asset");
            def = card;
            _allCards.Add(id,def);
            TryDownloadSounds(id);
            return true;
        }
    }

    public void TryDownloadSounds(string cardID)
    {
        foreach (CardSoundType type in System.Enum.GetValues(typeof(CardSoundType)))
        {
            DownloadSound(cardID, type);
        }
    }

    private void DownloadSound(string cardID, CardSoundType soundType)
    {
        
    }
}
