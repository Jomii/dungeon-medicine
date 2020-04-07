using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAOE : MonoBehaviour
{
  public float duration = 2.0f;
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
  void OnTriggerStay2D(Collider2D other)
  {
    EnemyController controller = other.GetComponent<EnemyController>();

    if (controller != null)
    {
      if (controller.health > 0)
      {
        controller.ChangeHealth(-1);
        Debug.Log(controller.name + " in aoe!");
      }
    }
  }
}
