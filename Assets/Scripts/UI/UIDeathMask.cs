using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeathMask : MonoBehaviour
{
  public static UIDeathMask instance { get; private set; }
  public GameObject mask;

  void Awake()
  {
    instance = this;
  }

  public void ShowDeathScreen()
  {
    // Debug.Log(gameObject.name);
    mask.SetActive(true);
  }
}
