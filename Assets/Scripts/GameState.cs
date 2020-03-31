using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
  public static GameState instance;

  public List<(Item, int)> inventoryItems = new List<(Item, int)>(6);
  public int health = 5;
  // public Inventory inventory;
  void Awake()
  {
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
