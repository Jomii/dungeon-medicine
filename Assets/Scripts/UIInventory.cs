using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
  public Transform itemsParent;
  public Color selectedItemColor = new Color(1f, 0.9480699f, 0.8820755f);
  Inventory inventory;
  InventorySlot[] slots;
  InventorySlot activeSlot;
  Color defaultItemColor = new Color(1f, 0.9480699f, 0.8820755f);

  // Start is called before the first frame update
  void Start()
  {
    inventory = Inventory.instance;
    inventory.onItemChangedCallback += UpdateUI;

    // Init slots and active slot
    slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    activeSlot = slots[inventory.selectedItemIndex];
    Image activeSlotImage = activeSlot.GetComponent<Image>();
    // Store default color and set selected color 
    defaultItemColor = activeSlotImage.color;
    activeSlotImage.color = selectedItemColor;
  }

  // Update is called once per frame
  void Update()
  {
    float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      UpdateSelectedItemIndex(0);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      UpdateSelectedItemIndex(1);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha3))
    {
      UpdateSelectedItemIndex(2);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha4))
    {
      UpdateSelectedItemIndex(3);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha5))
    {
      UpdateSelectedItemIndex(4);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha6))
    {
      UpdateSelectedItemIndex(5);
    }
    else if (mouseScroll != 0)
    {
      activeSlot.GetComponent<Image>().color = defaultItemColor;
      inventory.AddSelectedItemIndex((int)mouseScroll);
      activeSlot = slots[inventory.selectedItemIndex];
      activeSlot.GetComponent<Image>().color = selectedItemColor;
    }

    if (Input.GetKeyDown(KeyCode.Q))
    {
      inventory.UseSelectedItem();
    }
  }

  // Change previous slot color to default and new to selected color
  void UpdateSelectedItemIndex(int index)
  {
    activeSlot.GetComponent<Image>().color = defaultItemColor;
    inventory.SetSelectedItemIndex(index);
    activeSlot = slots[inventory.selectedItemIndex];
    activeSlot.GetComponent<Image>().color = selectedItemColor;
  }
  void UpdateUI()
  {
    for (int i = 0; i < slots.Length; i++)
    {
      if (i < inventory.items.Count)
      {
        if (inventory.items[i].Item1 != null)
        {
          slots[i].AddItem(inventory.items[i].Item1, inventory.items[i].Item2);
        }
        else
        {
          slots[i].ClearSlot();
        }
      }
      else
      {
        slots[i].ClearSlot();
      }
    }
  }
}
