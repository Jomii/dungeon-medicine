using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindingAOE : MonoBehaviour
{
  [Tooltip("Potion AOE effect duration")]
  public float duration = 0.5f;
  [Tooltip("How long target is blind")]
  public float blindDuration = 2.0f;
  float timer;

  void Awake()
  {
    timer = duration;
  }
  void Update()
  {
    timer -= Time.deltaTime;

    if (timer <= 0)
    {
      Destroy(gameObject);
    }
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    EnemyController controller = other.GetComponent<EnemyController>();

    if (controller != null)
    {
      Debug.Log(controller.name + " is blind!");
      controller.Blind(blindDuration);
    }
  }
}
