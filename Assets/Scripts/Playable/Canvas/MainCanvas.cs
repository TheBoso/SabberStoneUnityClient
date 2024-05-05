using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
  public static MainCanvas instance = null;

  private Canvas _canvas;


  public static Canvas Canvas
  {
    get
    {
      return instance._canvas;
    }
  }
  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
      _canvas = GetComponent<Canvas>();
    }
    else
    {
      Destroy(gameObject);
    }
  }
}
