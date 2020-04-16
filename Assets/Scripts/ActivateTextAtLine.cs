using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public TextAsset Text;

    public int startLine;
    public int endLine;

    public TextBoxManager2 theTextBox;

    public bool destroyWhenActivated;


    // Start is called before the first frame update
    void Start()
    {
     theTextBox = FindObjectOfType<TextBoxManager2>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            theTextBox.ReloadScript(Text);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;

            if(destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }
}
