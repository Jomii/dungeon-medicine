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
    Vector2 hotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
    Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
  }

}
