﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
  new public string name = "New Item";
  public GameObject prefab;
  public Sprite icon = null;
  public Color iconColor;
  [Tooltip("Maximum amount a player can carry")]
  public int stackSize = 1;
  [Tooltip("When used is the whole stack consumed?")]
  public bool useStack = false;
  public AudioClip craftSound;
  public List<Item> ingredients;

  // Return true if something happened
  public virtual bool Use()
  {
    // Use the item
    // Something might happen

    Debug.Log("Using " + name);
    return false; // Default item doesn't do anything
  }

  public virtual void Craft()
  {
    Debug.Log("Crafting " + name);
    if (craftSound)
    {
      GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlaySound(craftSound);
    }

    foreach (var item in ingredients)
    {
      Inventory.instance.removeItem(item.name);
    }

    Inventory.instance.Add(this, 1);
  }
}
