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
      controller.Save();
      StartCoroutine(LoadSceneAsync());
    }
  }

  IEnumerator LoadSceneAsync()
  {
    AsyncOperation asyncLoad;

    if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
    {
      // After last scene, return to the first scene
      asyncLoad = SceneManager.LoadSceneAsync(0);
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
