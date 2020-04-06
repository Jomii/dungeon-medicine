using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
  public static Inventory instance;
  public delegate void OnItemChanged();
  public OnItemChanged onItemChangedCallback;
  public (Item, int) rangedItem = (null, 0);
  public List<(Item, int)> items = new List<(Item, int)>(6);
  (Item, int) swapItem = (null, 0);

  void Awake()
  {

    if (instance == null)
    {
      DontDestroyOnLoad(gameObject);

      for (int i = 0; i < 6; i++)
      {
        items.Add((null, 0));
      }
      instance = this;
    }
    else if (instance != this)
    {
      List<(Item, int)> gamestateItems = GameState.instance.inventoryItems;

      if (gamestateItems.Count > 0)
      {
        // Set items to match GameState
        instance.items = new List<(Item, int)>(gamestateItems);
      }
      else
      {
        // No items saved in GameState, reset inventory
        instance.items.Clear();

        for (int i = 0; i < 6; i++)
        {
          instance.items.Add((null, 0));
        }
      }

      Destroy(gameObject);
    }

  }

  public int selectedItemIndex = 0;
  int space = 6;
  int usedSpace = 0;

  /* Add item to inventory, returns 
    true if item was added succesfully. */
  public int Add(Item item, int amount)
  {

    int i = items.FindIndex(x => x.Item1 != null && x.Item1.name == item.name && x.Item2 < x.Item1.stackSize);
    // If item in inventory try to add amount to its stacksize
    if (i != -1)
    {
      int stackSize = items[i].Item2;
      // Item's stacks are full and no room in inventory
      if (stackSize == item.stackSize)
      {
        if (usedSpace == space)
        {
          return 0;
        }
      }
      else if (stackSize + amount <= item.stackSize)
      {
        // Stack not full, add amount to stack
        items[i] = (item, items[i].Item2 + amount);

        UpdateUI();

        return amount;
      }
      else
      {
        // Room in stack, but not for full amount
        int addedAmount = amount - items[i].Item2;
        items[i] = (item, item.stackSize);
        UpdateUI();
        return addedAmount + Add(item, amount - stackSize);
      }
    }
    else if (usedSpace == space)
    {
      Debug.Log("Inventory full!");
      return 0;
    }

    i = items.FindIndex(x => x.Item1 == null);
    items[i] = (item, amount);
    usedSpace++;

    UpdateUI();

    return amount;
  }

  public void UseSelectedItem()
  {
    (Item, int) selectedStack = items[selectedItemIndex];

    if (selectedStack.Item1 == null)
    {
      return;
    }

    if (selectedStack.Item1.useStack || selectedStack.Item2 - 1 < 1)
    {
      selectedStack.Item1.Use();

      if (swapItem.Item1 == null)
      {
        items[selectedItemIndex] = (null, 0);
        usedSpace--;
      }
      else
      {
        items[selectedItemIndex] = swapItem;
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

  public void removeItem(string name)
  {
    for (int i = items.Count - 1; i >= 0; i--)
    {
      if (items[i].Item1 == null)
      {
        continue;
      }

      if (items[i].Item1.name == name)
      {

        if (items[i].Item2 - 1 <= 0)
        {
          items[i] = (null, 0);
          usedSpace--;
        }
        else
        {
          items[i] = (items[i].Item1, items[i].Item2 - 1);
        }

        return;
      }
    }
  }

  public void DropSelectedItem(Vector2 playerPosition)
  {
    (Item, int) itemStack = items[selectedItemIndex];
    if (itemStack.Item1 == null)
    {
      return;
    }

    GameObject collectibleObject = Instantiate(itemStack.Item1.prefab, playerPosition + Vector2.up, Quaternion.identity);
    Collectible collectible = collectibleObject.GetComponent<Collectible>();
    collectible.SetStackSize(itemStack.Item2);

    items[selectedItemIndex] = (null, 0);
    usedSpace--;
    UpdateUI();
  }

  public void SetRanged()
  {
    if (rangedItem.Item1 != null)
    {
      swapItem = rangedItem;
    }
    else
    {
      swapItem = (null, 0);
    }

    rangedItem = items[selectedItemIndex];
  }

  public void UseRanged()
  {
    rangedItem = (rangedItem.Item1, rangedItem.Item2 - 1);

    if (rangedItem.Item2 <= 0)
    {
      rangedItem = (null, 0);
    }

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

  public bool IsFull(string name)
  {
    int i = items.FindIndex(x => x.Item1 != null && x.Item1.name == name && x.Item2 < x.Item1.stackSize);

    if (i == -1 && usedSpace >= space)
    {
      return true;
    }
    else
    {
      return false;
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
