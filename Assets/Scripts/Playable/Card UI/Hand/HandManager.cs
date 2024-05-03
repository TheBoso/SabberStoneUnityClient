using System;
using SabberStoneCore.Model;
using UnityEngine;
using SabberStoneCore.Model.Entities;

public class HandManager : MonoBehaviour
{
   private Game _game;
   private Controller _player1;
   private PowerInterpreter _inter;
   [field: SerializeField] public RectTransform MainCanvas { get; private set; }


   public Controller Player => _player1;
   public PowerInterpreter PowerInterpreter => _inter;

   private void Awake()
   {
      _inter = FindObjectOfType<PowerInterpreter>();
   }

   public void Setup(Controller p1, Game game)
   {
      _game = game;
      _player1 = p1;
   }

   public bool IsPlayable(Transform obj)
   {
      for (int i = 0; i < transform.childCount; i++)
      {
         Transform child = transform.GetChild(i);
         Debug.Log(child.name);
         if (child.Equals(obj))
         {
            return _player1.HandZone[i].IsPlayable;
         }
      }

      return false;
   }

   public bool IsPlayable(int cardIndex)
   {
      if (cardIndex == -1) return false;
      return _player1.HandZone[cardIndex].IsPlayable;
   }

   public int GetHandCardIndex(Transform obj)
   {
      for (int i = 0; i < transform.childCount; i++)
      {
         Transform child = transform.GetChild(i);
         Debug.Log(child.name);
         if (child == obj)
         {
            return i;
         }
      }

      return -1;
   }

   public IPlayable GetPlayable(Transform obj)
   {
      for (int i = 0; i < transform.childCount; i++)
      {
         if (transform.GetChild(i) == obj)
         {
            return _player1.HandZone[i];
         }
      }

      return null;
   }

   public IPlayable GetPlayable(int cardIndex)
   {
      if (cardIndex == -1) return null;
      return _player1.HandZone[cardIndex];
   }
   public void Update()
   {
      if (_game == null) return;
      for (int i = 0; i < _player1.HandZone.Count; i++)
      {
         if (i >= transform.childCount)
         {
            return;
            
         }
         Transform card = transform.GetChild(i);
         if (card != null)
         {
            if (card.TryGetComponent(out CardGen gen))
            {
                gen.SetPlayableGlow(_player1.HandZone[i].IsPlayable);
            }
         }
      }
   }
}
