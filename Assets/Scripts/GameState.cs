﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
  public static GameState instance;
  public List<(Item, int)> inventoryItems = new List<(Item, int)>(6);
  public (Item, int) rangedItem = (null, 0);
  public int usedInventorySpace = 0;
  public int health = 5;
  public bool playerUsedCure;
  void Awake()
  {
    if (instance == null)
    {
      DontDestroyOnLoad(gameObject);
      instance = this;
    }
    else if (instance != this && SceneManager.GetActiveScene().buildIndex == 2)
    {
      instance.inventoryItems = new List<(Item, int)>(6);
      instance.health = 5;
      instance.usedInventorySpace = 0;
      rangedItem = (null, 0);
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }
  }
}
