using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager2 : MonoBehaviour
{
  public GameObject textBox;

  public Text Text;

  public TextAsset textFile;
  public string[] textLines;

  public int currentLine;
  public int endAtLine;

  public PlayerController player;

  public bool isActive;

  // Use this for initialization
  void Start()
  {
    player = FindObjectOfType<PlayerController>();

    if (textFile != null)
    {
      textLines = (textFile.text.Split('\n'));

    }

    if (endAtLine == 0)
    {
      endAtLine = textLines.Length - 1;

    }

    if(isActive)
    {
      EnableTextBox();
    }
    else
    {
      DisableTextBox();
    }

  }

  void Update()
  {

    if(!isActive)
    {
      return;
    }

    Text.text = textLines[currentLine];

    if (Input.GetKeyDown(KeyCode.Return))
    {
      currentLine += 1;
  
    }

    if (currentLine > endAtLine)
    {
      DisableTextBox();
    }
  }

  public void EnableTextBox()
  {
    textBox.SetActive(true);
  }

  public void DisableTextBox()
  {
    textBox.SetActive(false);
  }

  public void ReloadScript(TextAsset Text)
  {
    if(Text != null)
    {
      textLines = new string[1];
      textLines = (Text.text.Split('\n'));
    }
  }
}