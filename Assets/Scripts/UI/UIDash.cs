using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDash : MonoBehaviour
{
  public static UIDash instance { get; private set; }

  void Awake()
  {
    instance = this;
  }

  public void SetCooldown(float seconds)
  {
    float remaining = Mathf.Round(seconds);
    TextMeshProUGUI dashText = gameObject.GetComponent<TextMeshProUGUI>();
    dashText.text = "Dash " + (remaining > 0 ? remaining.ToString() : "");
  }
}
