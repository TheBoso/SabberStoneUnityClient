using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class OnMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 cachedScale;
    Vector3 cachedPosition;
    Quaternion cachedRotation;

    [FormerlySerializedAs("_back")] [SerializeField] private GameObject _front;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_front.activeSelf == false) return;
        cachedScale = transform.localScale;
        cachedPosition = transform.position;
        cachedRotation = transform.rotation;

        transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        transform.SetPositionAndRotation(new Vector3(cachedPosition.x, cachedPosition.y + ((cachedPosition.y > 450 ? -1 : 1) * 100), cachedPosition.z), Quaternion.identity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_front.activeSelf == false) return;

        transform.localScale = cachedScale;
        transform.SetPositionAndRotation(cachedPosition, Quaternion.identity);
    }
}