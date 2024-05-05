using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Model;
using SabberStoneCore.Model.Entities;
using SabberStoneCore.Tasks.PlayerTasks;
using TMPro;
using UnityEngine;

public class SelectedMinionManager : MonoBehaviour
{
   public static SelectedMinionManager instance = null;
   private EntityExt _selectedEntity;

   public static EntityExt SelectedMinion => instance._selectedEntity;
   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }

   private void Update()
   {
      if (instance._selectedEntity != null)
      {
         if (Input.GetMouseButtonDown(0))
         {
            if (BasicGen.MousedOverEntity == null) return;

            ICharacter ourSelectedCharacter =
               PowerInterpreter.instance.Game.Characters.FirstOrDefault(x => x.Id == instance._selectedEntity.Id);

            if (ourSelectedCharacter == null) return;
            
            // figure out if we can attack this.
           //  I guess we can do it this way?

           ICharacter target =
              PowerInterpreter.instance.Game.Characters.FirstOrDefault(x => x.Id == BasicGen.MousedOverEntity.Id);

           if (target != null && ourSelectedCharacter.IsValidAttackTarget(target))
           {
              if (ourSelectedCharacter is Minion)
              {
                 PowerInterpreter.instance.GameProcessWrapper(MinionAttackTask.Any(ourSelectedCharacter.Controller, ourSelectedCharacter,target));

              }
              else if (ourSelectedCharacter is Hero)
              {
                  PowerInterpreter.instance.GameProcessWrapper(HeroAttackTask.Any(ourSelectedCharacter.Controller, target));
              }
           }
         }
         if (Input.GetMouseButtonDown(1))
         {
            DeselectMinion();
         }
      }
  
   }

   public static void SelectMinion(EntityExt entity)
   {
      SelectableMinion selectable = null;
      if (instance._selectedEntity != null)
      {
         if(instance._selectedEntity.GameObjectScript.TryGetComponent(out selectable))
         {
            selectable.DeselectMinion();
         }
      }
      
      instance._selectedEntity = entity;
      if (instance._selectedEntity.GameObjectScript.TryGetComponent(out selectable))
      {
         selectable.SelectMinion();
      }
   }

   public static void DeselectMinion()
   {
      if (instance._selectedEntity != null)
      {
         if (instance._selectedEntity.GameObjectScript.TryGetComponent(out SelectableMinion minion))
         {
          minion.DeselectMinion();  
         }
      }
      instance._selectedEntity = null;
   }
}
