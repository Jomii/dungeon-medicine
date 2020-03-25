using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Inventory : MonoBehaviour
{
  public static Inventory instance;
  public delegate void OnItemChanged();
  public OnItemChanged onItemChangedCallback;
  public List<(Item, int)> items = new List<(Item, int)>(6);

  void Awake()
  {
    if (instance != null)
    {
      Debug.LogWarning("More than one instance of Inventory found!");
      return;
    }

    for (int i = 0; i < 6; i++)
    {
      items.Add((null, 0));
    }

    instance = this;
  }

  public int selectedItemIndex = 0;
  int space = 6;

  /* Add item to inventory, returns 
    true if item was added succesfully. */
  public bool Add(Item item)
  {

    int i = items.FindLastIndex(x => x.Item1 != null && x.Item1.name == item.name);
    // If item in inventory try to add 1 to its stacksize
    if (i != -1)
    {
      int stackSize = items[i].Item2;
      // Item's stacks are full and no room in inventory
      if (stackSize == item.stackSize)
      {
        if (space <= 0)
        {
          return false;
        }
      }
      else
      {
        // Stack not full, add 1 to stack
        items[i] = (item, items[i].Item2 + 1);

        UpdateUI();

        return true;
      }
    }
    else if (space <= 0)
    {
      return false;
    }

    i = items.FindIndex(x => x.Item1 == null);
    items[i] = (item, 1);
    space--;

    UpdateUI();

    return true;
  }

  public void UseSelectedItem()
  {
    (Item, int) selectedStack = items[selectedItemIndex];

    if (selectedStack.Item1 == null)
    {
      return;
    }

    if (selectedStack.Item2 - 1 < 1)
    {
      if (selectedStack.Item1 != null)
      {
        selectedStack.Item1.Use();
        items[selectedItemIndex] = (null, 0);
      }
    }
    else
    {
      Item item = selectedStack.Item1;
      item.Use();
      items[selectedItemIndex] = (item, selectedStack.Item2 - 1);
    }

    UpdateUI();
  }

  public void Remove(Item item)
  {
    // items.Remove(item);
    UpdateUI();
  }

  public void AddSelectedItemIndex(int value)
  {
    selectedItemIndex += value;

    if (selectedItemIndex > space - 1)
    {
      selectedItemIndex = 0;
    }
    else if (selectedItemIndex < 0)
    {
      selectedItemIndex = space - 1;
    }
  }

  public void SetSelectedItemIndex(int value)
  {
    if (value >= 0 && value < space)
    {
      selectedItemIndex = value;
    }
  }

  void UpdateUI()
  {
    if (onItemChangedCallback != null)
    {
      onItemChangedCallback.Invoke();
    }
  }
}
