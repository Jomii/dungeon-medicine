using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursor : MonoBehaviour
{
  public static UICursor instance { get; private set; }
  public Texture2D cursorTexture;
  void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
  }

}
