using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "The Cure", menuName = "Inventory/Cure")]
public class TheCure : Item
{
  public override bool Use()
  {
    Debug.Log("Player used the cure");
    GameState.instance.playerUsedCure = true;

    return true;
  }
}
