using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHelper : MonoBehaviour
{
 public static BoardHelper instance = null;
 private RectTransform _rect;

 private void Awake()
 {
  if (instance == null)
  {
   instance = this;
   _rect = transform as RectTransform;
  }
  else
  {
   Destroy(gameObject);
  }
 }
 
 
 public static bool IsWithinBounds(Vector2 point)
 {
  return instance._rect.rect.Contains(point);
 }
}
