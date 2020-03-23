using UnityEngine;

public class Collectible : MonoBehaviour
{
  public Item item;
  public int amount = 1;
  public AudioClip collectedClip;

  public virtual void Collect()
  {
    Debug.Log("Collected " + item.name);
    Inventory.instance.Add(item);
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    PlayerController controller = other.GetComponent<PlayerController>();

    if (controller != null)
    {
      Collect();

      Destroy(gameObject);
      controller.PlaySound(collectedClip);
    }
  }
}
