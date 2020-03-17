using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
  void OnTriggerStay2D(Collider2D other)
  {
    PlayerController controller = other.GetComponent<PlayerController>();

    if (controller != null)
    {
      if (controller.health > 0)
      {
        controller.ChangeHealth(-1);
      }
    }
  }
}
