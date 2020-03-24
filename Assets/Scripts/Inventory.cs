using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
  public static Inventory instance;
  void Awake()
  {
    if (instance != null)
    {
      Debug.LogWarning("More than one instance of Inventory found!");
      return;
    }
    instance = this;
  }

  public List<(Item, int)> items = new List<(Item, int)>();
  int space = 1;

  public void Add(Item item)
  {

    int i = items.FindLastIndex(x => x.Item1.name == item.name);
    if (i != -1)
    {
      int stackSize = items[i].Item2;
      if (stackSize == item.stackSize)
      {
        Debug.Log("Current stack full, adding to next slot");
        if (items.Count == space)
        {
          Debug.Log("Invetory full!");
        }
        else
        {
          items.Add((item, 1));
        }
      }
      else
      {
        items[i] = (item, items[i].Item2 + 1);
      }
    }
    else
    {
      if (items.Count == space)
      {
        Debug.Log("Inventory full!");
      }
      else
      {
        items.Add((item, 1));
      }
    }

    items.ForEach(x => Debug.Log(x.Item1.name + " amount: " + x.Item2));
  }

  public void Remove(Item item)
  {
    // items.Remove(item);
  }
}
