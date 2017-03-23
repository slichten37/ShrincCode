using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
    public Text theText;

    public TextAsset textFile;
    public string[] textLines;
    public int currentLine;
    public int next;

    public bool isActive;

    public PlayerMovement player;

    public bool stopPlayerMovement;

    public ActivateTextAtLine currentCaller;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if(isActive)
        {
            EnableTextBox();
        } else
        {
            DisableTextBox();
        }


        //theText.position = textBox.transform;

    }

    void Update()
    {

        if(!isActive)
        {
            return;
        }

        if (textLines[currentLine].Contains("*"))
        {
            theText.text = textLines[currentLine].Substring(3, textLines[currentLine].Length - 8) + "\n" + textLines[currentLine + 1].Substring(3, textLines[currentLine + 1].Length - 8) + "\n" + textLines[currentLine + 2].Substring(3, textLines[currentLine + 2].Length - 8) + "\n" + textLines[currentLine + 3].Substring(3, textLines[currentLine + 3].Length - 8);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                int x = textLines[currentLine].Length;
                next = int.Parse(textLines[currentLine].Substring(x - 5, 3));
                setNext(next);

            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                int x = textLines[currentLine + 1].Length;
                next = int.Parse(textLines[currentLine + 1].Substring(x - 5, 3));
                setNext(next);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                int x = textLines[currentLine + 2].Length;
                next = int.Parse(textLines[currentLine + 2].Substring(x - 5, 3));
                setNext(next);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                int x = textLines[currentLine + 3].Length;
                next = int.Parse(textLines[currentLine + 3].Substring(x - 5, 3));
                setNext(next);
            }
        }
        else if (textLines[currentLine].Contains("$"))
        {
            theText.text = textLines[currentLine].Substring(3, textLines[currentLine].Length - 8);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                int x = textLines[currentLine].Length;

                next = int.Parse(textLines[currentLine].Substring(x - 5, 3));
                setNext(next);
            }
        }
        else
        {
            theText.text = textLines[currentLine].Substring(3, textLines[currentLine].Length - 4);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DisableTextBox();
            }
        }
        

    }

    public void EnableTextBox()
    {

        textBox.SetActive(true);
        isActive = true;

        if (stopPlayerMovement)
        {
            player.canMove = false;
        }
    }

    public void DisableTextBox()
    {

        textBox.SetActive(false);
        isActive = false;
        player.canMove = true;
    }

    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));

        }
    }

    public void setNext(int x)
    {
        String s = x.ToString();
        while (s.Length < 3)
        {
            s = "0" + s;
        }
        for (int i = 0; i < textLines.Length; i++)
        {
            if (textLines[i].StartsWith(s))
            {
                currentLine = i;
                return;
            }
        }
    }
}
