using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{

  public GameObject dialogBox;
  public bool isLastLevel;
  bool playerInRange = false;

  // Start is called before the first frame update
  void Start()
  {
    dialogBox.SetActive(false);
  }

  void Update()
  {
    if (playerInRange && Input.GetKeyDown(KeyCode.E))
    {
      if (isLastLevel)
      {
        UICrafting.instance.finalItemEnabled = true;
      }

      UICrafting.instance.ToggleVisible();
    }
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

      if (UICrafting.instance.enabled)
      {
        UICrafting.instance.ToggleVisible();
      }
    }
  }
}
