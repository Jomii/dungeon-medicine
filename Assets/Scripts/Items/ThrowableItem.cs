﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Throwable", menuName = "Inventory/Throwable")]
public class ThrowableItem : Item
{
  public GameObject projectilePrefab;

  public override bool Use()
  {
    Inventory.instance.SetRanged();

    PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    playerController.Equip(projectilePrefab);

    return true;
  }
}
