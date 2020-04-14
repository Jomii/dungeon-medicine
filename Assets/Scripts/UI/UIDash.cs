using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDash : MonoBehaviour
{
  public static UIDash instance { get; private set; }
  public Image mask;
  float originalSize;

  void Awake()
  {
    instance = this;
    originalSize = mask.rectTransform.rect.width;
  }


  public void SetCooldown(float seconds)
  {
    float remaining = Mathf.Round(seconds);
    Debug.Log("remaining cd_ " + seconds);
    mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * seconds);
  }
}
