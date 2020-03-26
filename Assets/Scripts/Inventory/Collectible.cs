using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
  public Item item;
  public int stackSize = 1;
  public AudioClip collectedClip;

  void Awake()
  {
    if (stackSize > 1)
    {
      TextMeshProUGUI stackText = GetComponentInChildren<TextMeshProUGUI>();
      stackText.text = stackSize.ToString();
      stackText.enabled = true;
    }
  }

  public void SetStackSize(int size)
  {
    stackSize = size;

    TextMeshProUGUI stackText = GetComponentInChildren<TextMeshProUGUI>();
    if (stackSize > 1)
    {
      stackText.text = stackSize.ToString();
      stackText.enabled = true;
    }
    else
    {
      stackText.text = "";
      stackText.enabled = false;
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    PlayerController controller = other.GetComponent<PlayerController>();

    if (controller != null)
    {
      int collectedAmount = Inventory.instance.Add(item, stackSize);

      if (collectedAmount == stackSize)
      {
        Destroy(gameObject);
        controller.PlaySound(collectedClip);
      }
      else
      {
        Debug.Log("Collected amount: " + collectedAmount);
        SetStackSize(stackSize - collectedAmount);
      }
    }
  }
}
