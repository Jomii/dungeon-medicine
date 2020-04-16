using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
  public GameObject dialogBox;
  bool playerInRange = false;
  // Start is called before the first frame update
  void Start()
  {
    dialogBox.SetActive(false);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    PlayerController player = other.GetComponent<PlayerController>();

    if (player != null)
    {
      dialogBox.SetActive(true);
      playerInRange = true;
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    PlayerController player = other.GetComponent<PlayerController>();

    if (player != null)
    {
      dialogBox.SetActive(false);
      playerInRange = false;
    }
  }
}
