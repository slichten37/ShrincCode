using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;
    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;

    public bool destroyWhenActivated;

    public bool requireButtonPress;
    public bool waitForButtonPress;


    // Use this for initialization
    void Start() {
        theTextBox = FindObjectOfType<TextBoxManager>();

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (requireButtonPress)
            {
                waitForButtonPress = true;
                return;
            }

            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

        }


        if (destroyWhenActivated)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }
}
