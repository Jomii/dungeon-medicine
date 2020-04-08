using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
  public Image icon;
  public TextMeshProUGUI stackText;
  public Item item;

  public void AddItem(Item newItem, int stackSize)
  {
    item = newItem;

    icon.enabled = true;
    icon.sprite = item.icon;

    if (stackSize > 1)
    {
      stackText.enabled = true;
      stackText.text = stackSize.ToString();
    }
    else
    {
      stackText.text = "";
    }
  }

  public void ClearSlot()
  {
    item = null;

    icon.enabled = false;
    icon.sprite = null;

    stackText.enabled = false;
    stackText.text = "";
  }
}

