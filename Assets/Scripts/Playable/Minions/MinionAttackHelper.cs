using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SabberStoneCore.Enums;
using UnityEngine;

public class MinionAttackHelper : MonoBehaviour
{
    private AnimationGen _minionGen;
    private Vector2 _originalScale;
    private void Awake()
    {
        _minionGen = GetComponent<AnimationGen>();
    }

    
    public IEnumerator AttackRoutine(EntityExt ent)
    {
        if (SelectedMinionManager.SelectedMinion == ent)
        {
            SelectedMinionManager.DeselectMinion();
        }
        
        Vector3 oldPosition = transform.position;
        int ourAttack = _minionGen._entityExt.Tags[GameTag.ATK];
        Transform target = ent.GameObjectScript.transform;
        Sequence seq = DOTween.Sequence();
        
        
        seq.Append(transform.DOMove(target.position, 0.5f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                Vector3 punchPower = Vector3.zero;
                if(ourAttack >= 8)
                {
                    punchPower = Vector3.one;
                }
                else if (ourAttack > 5)
                {
                    punchPower = Vector3.one * 0.5f;
                }
                else
                {
                    punchPower = Vector3.one * 0.25f;
                }

                target.DOShakePosition(0.25f, punchPower);
            }));

        seq.Append(transform.DOMove(oldPosition, 0.25f).SetEase(Ease.InOutSine));
        
        Debug.Log("Starting Minion Attack");
        yield return seq.WaitForCompletion();
        Debug.Log("Ending Minion Attack");
    }
}
