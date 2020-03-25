using UnityEngine;

public class Collectible : MonoBehaviour
{
  public Item item;
  public AudioClip collectedClip;

  void OnTriggerEnter2D(Collider2D other)
  {
    PlayerController controller = other.GetComponent<PlayerController>();

    if (controller != null)
    {
      bool wasCollected = Inventory.instance.Add(item);

      if (wasCollected)
      {
        Destroy(gameObject);
        controller.PlaySound(collectedClip);
      }
    }
  }
}
