using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
  new public string name = "New Item";
  public GameObject prefab;
  public Sprite icon = null;
  public int stackSize = 1;
  [Tooltip("When used is the whole stack consumed?")]
  public bool useStack = false;
  public List<Item> ingredients;

  public virtual void Use()
  {
    // Use the item
    // Something might happen

    Debug.Log("Using " + name);
  }

  public virtual void Craft()
  {
    Debug.Log("Crafting " + name);
  }
}
