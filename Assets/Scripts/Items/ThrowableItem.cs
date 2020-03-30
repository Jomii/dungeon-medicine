using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Throwable", menuName = "Inventory/Throwable")]
public class ThrowableItem : Item
{
  public GameObject projectilePrefab;

  public override void Use()
  {
    Inventory.instance.SetRanged();

    PlayerController playerController = Inventory.instance.GetComponent<PlayerController>();
    playerController.Equip(projectilePrefab);
  }
}
