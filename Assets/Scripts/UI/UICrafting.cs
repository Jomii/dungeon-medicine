﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICrafting : MonoBehaviour
{
  public static UICrafting instance { get; private set; }
  public Transform itemsParent;
  public Color selectedItemColor = Color.white;
  public TextMeshProUGUI craftText;
  public bool finalItemEnabled { get; set; }
  Inventory inventory;
  InventorySlot activeSlot;
  int selectedSlotIndex = 0;
  InventorySlot[] slots;
  Color defaultItemColor = new Color(0.8867924f, 0.7217246f, 0.5061409f);
  Color warningColor;
  float craftTime = 1.0f;
  float craftTimer;
  Image mask;
  float originalSize;
  bool ingredients;

  void Awake()
  {
    instance = this;
    ToggleVisible();
  }

  // Start is called before the first frame update
  void Start()
  {
    inventory = Inventory.instance;

    slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    activeSlot = slots[selectedSlotIndex];

    activeSlot.GetComponent<Image>().color = selectedItemColor;
    mask = activeSlot.transform.Find("Mask").GetComponent<Image>();

    craftTimer = craftTime;
    originalSize = mask.rectTransform.rect.width;

    craftText = transform.Find("CraftText").GetComponent<TextMeshProUGUI>();
  }

  // Update is called once per frame
  void Update()
  {

    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
    {
      this.enabled = false;
      this.transform.gameObject.SetActive(false);
    }

    int mouseScroll = (int)Input.GetAxis("Mouse ScrollWheel");

    if (mouseScroll != 0)
    {
      if (selectedSlotIndex + mouseScroll < slots.Length && selectedSlotIndex + mouseScroll >= 0)
      {
        activeSlot.GetComponent<Image>().color = defaultItemColor;
        selectedSlotIndex += mouseScroll;
        activeSlot = slots[selectedSlotIndex];
        activeSlot.GetComponent<Image>().color = selectedItemColor;
        mask = activeSlot.transform.Find("Mask").GetComponent<Image>();
      }
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      ingredients = AllIngredients();

      if (!ingredients || Inventory.instance.IsFull(activeSlot.item.name))
      {
        CannotCraft();
      }
      mask.enabled = true;
    }

    if (Input.GetKey(KeyCode.Space))
    {
      if (craftTimer > 0 && ingredients && !Inventory.instance.IsFull(activeSlot.item.name))
      {
        CraftSelected();
      }
      else
      {
        craftTimer = craftTime;
        CannotCraft();
      }
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
      mask.color = defaultItemColor;
      mask.enabled = false;
      craftTimer = craftTime;

      craftText.color = Color.white;
      craftText.text = "Press [space] to craft";
    }
  }

  public void ToggleVisible()
  {
    this.enabled = !this.enabled;
    this.transform.gameObject.SetActive(this.enabled);
  }

  // Return true if all ingredients of active slot exist in inventory 
  bool AllIngredients()
  {
    List<(Item, int)> ingredientList = new List<(Item, int)>();

    if (!activeSlot.item || activeSlot.item.ingredients.Count <= 0)
    {
      return false;
    }

    for (int k = 0; k < activeSlot.item.ingredients.Count; k++)
    {
      Item current = activeSlot.item.ingredients[k];
      int requiredAmount = 0 + activeSlot.item.ingredients.FindAll(x => x != null && x.name == current.name).Count;
      ingredientList.Add((current, requiredAmount));
    }


    for (int i = 0; i < ingredientList.Count; i++)
    {
      List<(Item, int)> items = inventory.items.FindAll(x => x.Item1 != null && x.Item1.name == ingredientList[i].Item1.name);
      int sum = 0;

      for (int j = 0; j < items.Count; j++)
      {
        sum += items[j].Item2;
      }

      if (sum < ingredientList[i].Item2)
      {
        return false;
      }

    }

    return true;
  }

  void CannotCraft()
  {
    mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize);
    mask.color = Color.red;
    craftText.color = Color.red;
    craftText.text = !ingredients ? "Not enough ingredients!" : "No room for item in inventory!";
  }

  void CraftSelected()
  {
    craftTimer -= Time.deltaTime;

    mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (craftTime - craftTimer / craftTime));

    if (craftTimer <= 0)
    {
      activeSlot.item.Craft();
      ingredients = AllIngredients();
      craftTimer = craftTime;
    }
  }
}
