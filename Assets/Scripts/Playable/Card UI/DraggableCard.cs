using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using SabberStoneCore.Model;
using SabberStoneCore.Model.Entities;
using SabberStoneCore.Tasks.PlayerTasks;
using UnityEngine.UI;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private static PowerInterpreter _interpreter;

    public static Game Game => _interpreter.Game;

    private HandManager _handManager;
    private Camera _cam;
    private bool isDragging = false;
    private Vector3 lastMousePosition;
    private Vector3 _initialPos;
    [SerializeField] private float rotationSpeed = 5.0f; // Adjust the rotation speed as needed
    [SerializeField] private float translationSpeed = 0.1f; // Adjust the translation speed as needed
    [SerializeField] private float maxRotationAngle = 30.0f; // Adjust this value as needed


    // public static readonly Vector2 PLAY_AREA_TOP_LEFT = new Vector2(133, 229);
    // public static readonly Vector2 PLAY_AREA_BOTTOM_RIGHT = new Vector2(648, 143);

    private RectTransform _rectTransform;
    private Transform _oldParent;
    private int _cardIndex = -1;

 


    private void Awake()
    {
        _cam = Camera.main;
        if (_interpreter == null)
        {
            _interpreter = FindObjectOfType<PowerInterpreter>();
        }

        _rectTransform = transform as RectTransform;
    }

    private void Start()
    {
        SetHandManager(FindObjectOfType<HandManager>());
    }

    public void SetHandManager(HandManager mgr)
    {
        _handManager = mgr;
    }

    public void CacheParent(Transform parent)
    {
        _oldParent = parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _cardIndex = _handManager.GetHandCardIndex(transform);
        if (_handManager.IsPlayable(_cardIndex) == false)
        {
            return;
        }

        Debug.Log("Begin Drag");
        transform.SetParent(_handManager.MainCanvas, true);
        _initialPos = transform.localPosition;
        //isDragging = true;
        lastMousePosition = Input.mousePosition;
        isDragging = true;
    }

    private void Update()
    {
        if (isDragging)
        {
            if (BoardHelper.IsWithinBounds(lastMousePosition))
            {
                Debug.Log("Within Bounds");
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Ended");
        isDragging = false;
        if (_handManager.IsPlayable(_cardIndex) == false)
        {
            return;
        }

        Vector3 pos = lastMousePosition;
        Debug.Log($"Our Position {pos}");
        if (BoardHelper.IsWithinBounds(pos))
        {
            IPlayable playable = _handManager.GetPlayable(_cardIndex);
            if (playable != null)
            {
                // perform summon / cast
                if (playable is Minion)
                {
                    // todo: stuff related to targetting?
                    _handManager.PowerInterpreter.GameProcessWrapper(new PlayCardTask(_handManager.Player, playable));
                }
            }
        }
        else
        {
            transform.SetParent(_oldParent);
            transform.SetSiblingIndex(_cardIndex);
            // if not, return to initial pos
            (transform as RectTransform).DOAnchorPos(Vector2.zero, 0.25f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(_oldParent as RectTransform);
            });
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            _rectTransform.anchoredPosition += eventData.delta;
            this.lastMousePosition = _rectTransform.anchoredPosition / MainCanvas.Canvas.scaleFactor;
        }
    }
}