using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedItem", menuName = "Inventory/SpeedItem")]
public class SpeedPotion : Item
{
  public float speed = 10.0f;
  public float duration = 4.0f;
  public override bool Use()
  {
    PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    playerController.SetSpeed(speed, duration);

    return true;
  }
}
