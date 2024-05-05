using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SabberStoneCore.Enums;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttackHelper : MonoBehaviour
{
    private AnimationGen _minionGen;
    private Vector2 _originalScale;
    private void Awake()
    {
        _minionGen = GetComponent<AnimationGen>();
    }

    
    public IEnumerator AttackRoutine(EntityExt ent)
    {
Debug.Log($"Attacker {_minionGen._entityExt.CardId}");
Debug.Log($"Defender {ent.CardId}", ent.GameObjectScript);

        if (SelectedMinionManager.SelectedMinion == ent)
        {
            SelectedMinionManager.DeselectMinion();
        }

        Transform oldParent = transform.parent;
        int ourAttack = _minionGen._entityExt.Tags[GameTag.ATK];
        Transform target = ent.GameObjectScript.transform;
        Transform rectTransform = transform;
        Vector3 oldPosition = rectTransform.position;
        Sequence seq = DOTween.Sequence();
        int index = transform.GetSiblingIndex();
      //  rectTransform.SetParent(null);
        
        //  tween to enemy character
        seq.Append(rectTransform.DOMove(target.position, 0.5f)
            .SetEase(Ease.InOutSine));

        seq.AppendCallback(() =>
        {
            Vector2 punchPower = Vector2.zero;
            AudioClip clip = null;
            if (ourAttack >= 8)
            {
                clip = GameSettings.Instance.CharacterDamageHeavySound;
                punchPower = Vector2.one;
            }
            else if (ourAttack > 5)
            {
                clip = GameSettings.Instance.CharacterDamageMediumSound;
                punchPower = Vector2.one * 0.75f;
            }
            else
            {
                clip = GameSettings.Instance.CharacterDamageLightSound;
                punchPower = Vector2.one * 0.2f;
            }

            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
            target.DOPunchScale(punchPower, 0.25f).SetEase(Ease.InOutSine);
        });
        //  tween back
        seq.Append(rectTransform.DOMove(oldPosition, 0.5f).SetEase(Ease.InOutSine));
        Debug.Log("Starting Minion Attack");
        yield return seq.Play().WaitForCompletion();
    //    transform.SetParent(oldParent);
    //    transform.SetSiblingIndex(index);
        LayoutRebuilder.ForceRebuildLayoutImmediate(oldParent as RectTransform);
        Debug.Log("Ending Minion Attack");
    }
}
