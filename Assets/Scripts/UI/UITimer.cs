using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
  public static UITimer instance { get; private set; }
  TextMeshProUGUI timerText;
  public int LevelTime = 60;
  int timer;

  void Awake()
  {
    timerText = gameObject.GetComponent<TextMeshProUGUI>();
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    timer = LevelTime;
    InvokeRepeating("DrawTimer", 0.0f, 1.0f);
  }

  void DrawTimer()
  {
    timer--;

    if (timer <= 0)
    {
      CancelInvoke(); // Stop InvokeRepeating calls
      timerText.color = Color.red;
    }

    timerText.text = timer.ToString();
  }
}
