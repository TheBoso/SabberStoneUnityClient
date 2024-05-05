using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SabberStoneCore.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableMinion : Button
{
    private EntityExt _ent;
    private Vector3 _originalScale;

    protected override void Start()
    {
        base.Start();
        _originalScale = transform.localScale;
        _ent = GetComponent<MinionGen>()._entityExt;

    }
    public void SelectMinion()
    {
        transform.DOScale(Vector2.one * 1.1f, 0.25f);
    }

    public void DeselectMinion()
    {
        transform.DOScale(_originalScale, 0.25f);
    }
    
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (_ent.Tags[GameTag.CONTROLLER] == 1 && _ent.IsExhausted == false)
        {
            SelectedMinionManager.SelectMinion(_ent);
        }
    }
}
