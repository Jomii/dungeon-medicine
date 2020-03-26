﻿
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
  new public string name = "New Item";
  public GameObject prefab;
  public Sprite icon = null;
  public int stackSize = 1;

  public virtual void Use()
  {
    // Use the item
    // Something might happen

    Debug.Log("Using " + name);
  }
}