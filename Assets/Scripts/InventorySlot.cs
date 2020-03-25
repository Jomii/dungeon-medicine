using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
  public Image icon;
  public TextMeshProUGUI stackText;
  Item item;

  public void AddItem(Item newItem, int stackSize)
  {
    item = newItem;

    icon.sprite = item.icon;
    icon.enabled = true;

    if (stackSize > 1)
    {
      stackText.enabled = true;
      stackText.text = stackSize.ToString();
    }
  }

  public void ClearSlot()
  {
    item = null;

    icon.sprite = null;
    icon.enabled = false;
  }
}

