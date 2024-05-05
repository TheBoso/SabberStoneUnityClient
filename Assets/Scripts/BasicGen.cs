using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCore.Model.Entities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum AnimationState
{
    NONE,
    ATTACK,
    DESTROY,
    HEALTHCHANGE,
    DEAD,
    DONE,
    TARGETING
}

public abstract class BasicGen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AnimationState AnimState { get; set; }

    internal EntityExt _entityExt;

    [SerializeField]
    private GameObject _mouseHoverGlow;

    private static EntityExt _mousedOverEntity;
    public static EntityExt MousedOverEntity => _mousedOverEntity;

    public virtual void UpdateEntity(EntityExt entityExt)
    {
        _entityExt = entityExt;
    }

    public int Tag(GameTag gameTag) => _entityExt.Tags[gameTag];

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_mouseHoverGlow != null)
        {
            _mouseHoverGlow.SetActive(true);
        }

        _mousedOverEntity = _entityExt;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mousedOverEntity = null;
        if (_mouseHoverGlow != null)
        {
            _mouseHoverGlow.SetActive(false);
        }    }
}
