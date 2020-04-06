using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HealItem", menuName = "Inventory/HealItem")]
public class HealItem : Item
{
  public override void Use()
  {
    PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    playerController.ChangeHealth(1);
  }

}
