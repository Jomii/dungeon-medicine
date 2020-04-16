using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTransition : MonoBehaviour
{
  public float displayContinue = 10.0f;
  public bool toMainMenu;
  public GameObject continueText;
  // Start is called before the first frame update
  void Start()
  {
    continueText.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (displayContinue >= 0)
    {
      displayContinue -= Time.deltaTime;

      if (Input.GetButtonDown("Jump"))
      {
        displayContinue = 0;
      }
    }
    else
    {
      continueText.SetActive(true);

      if (Input.GetButtonDown("Jump"))
      {
        if (toMainMenu)
        {
          SceneManager.LoadScene(0);
        }
        else
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
      }
    }
  }
}
