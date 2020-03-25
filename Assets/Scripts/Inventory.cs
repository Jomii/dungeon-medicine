using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
  public static Inventory instance;
  public delegate void OnItemChanged();
  public OnItemChanged onItemChangedCallback;

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
  int space = 2;

  /* Add item to inventory, returns 
    true if item was added succesfully. */
  public bool Add(Item item)
  {

    int i = items.FindLastIndex(x => x.Item1.name == item.name);
    // If item in inventory try to add 1 to its stacksize
    if (i != -1)
    {
      int stackSize = items[i].Item2;
      // Item's stacks are full and no room in inventory
      if (stackSize == item.stackSize)
      {
        if (items.Count == space)
        {
          return false;
        }
      }
      else
      {
        // Stack not full, add 1 to stack
        items[i] = (item, items[i].Item2 + 1);

        if (onItemChangedCallback != null)
        {
          onItemChangedCallback.Invoke();
        }

        return true;
      }
    }
    else if (items.Count == space)
    {
      return false;
    }

    items.Add((item, 1));

    if (onItemChangedCallback != null)
    {
      onItemChangedCallback.Invoke();
    }

    return true;
  }

  public void Remove(Item item)
  {
    // items.Remove(item);
    if (onItemChangedCallback != null)
    {
      onItemChangedCallback.Invoke();
    }
  }
}
