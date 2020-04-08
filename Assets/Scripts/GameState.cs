using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
  public static GameState instance;
  public List<(Item, int)> inventoryItems = new List<(Item, int)>(6);
  public (Item, int) rangedItem = (null, 0);
  public int health = 5;
  // public Inventory inventory;
  void Awake()
  {
    if (instance != this && SceneManager.GetActiveScene().buildIndex == 2)
    {
      instance.inventoryItems = new List<(Item, int)>(6);
      instance.health = 5;
      rangedItem = (null, 0);
    }

    if (instance == null)
    {
      DontDestroyOnLoad(gameObject);
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }
  }
}
