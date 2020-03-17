using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D other)
  {
    PlayerController controller = other.GetComponent<PlayerController>();

    if (controller != null)
    {
      Debug.Log("IM HIT!");
      StartCoroutine(LoadSceneAsync());
    }
  }

  IEnumerator LoadSceneAsync()
  {
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level1");

    while (!asyncLoad.isDone)
    {
      yield return null;
    }
  }
}
