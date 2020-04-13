using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
  public static LevelExit instance { get; private set; }
  public GameObject enemyPrefab;
  public float spawnRate = 3.0f;
  public Vector3 playerSpawnPos { get; set; }
  public bool isLastLevel;
  void Awake()
  {
    instance = this;
  }
  public void SpawnEnemies()
  {
    InvokeRepeating("SpawnEnemy", 0.0f, spawnRate);
  }

  void SpawnEnemy()
  {
    Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    Instantiate(enemyPrefab, playerSpawnPos, Quaternion.identity);
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    PlayerController controller = other.GetComponent<PlayerController>();

    if (controller != null)
    {
      controller.Save();
      StartCoroutine(LoadSceneAsync());
    }
  }

  IEnumerator LoadSceneAsync()
  {
    AsyncOperation asyncLoad;

    if (isLastLevel)
    {
      (Item, int) cure = Inventory.instance.items.Find(x => x.Item1 != null && x.Item1.name == "The Cure");

      if (GameState.instance.playerUsedCure)
      {
        asyncLoad = SceneManager.LoadSceneAsync("GreedyEnding");
      }
      else if (cure.Item1 != null)
      {
        asyncLoad = SceneManager.LoadSceneAsync("GoodEnding");
      }
      else
      {
        asyncLoad = SceneManager.LoadSceneAsync("ClumsyEnding");
      }

    }
    else
    {
      // Load the next scene in build stack
      asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    while (!asyncLoad.isDone)
    {
      yield return null;
    }
  }
}
