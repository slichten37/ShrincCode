﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
    public Text theText;

    public TextAsset textFile;
    public string[] textLines;
    public int currentLine;
    public int endAtLine;

    public bool isActive;

    public PlayerMovement player;

    public bool stopPlayerMovement;

    public ActivateTextAtLine currentCaller;

    public int nextJump;

    // Use this for initialization
    void Start()
    {
        nextJump = 1;
        player = FindObjectOfType<PlayerMovement>();

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

        if (textLines[currentLine].StartsWith("Z:"))
        {
            theText.text = textLines[currentLine] + "\n" + textLines[currentLine + 1] + "\n" + textLines[currentLine + 2];
            if (Input.GetKeyDown(KeyCode.Z))
            {
                currentLine += 3;
                nextJump = 3;

            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                currentLine += 4;
                nextJump = 2;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                currentLine += 5;
                nextJump = 1;
            }
        }
        else
        {
            theText.text = textLines[currentLine];

            if (Input.GetKeyDown(KeyCode.Return))
            {
                currentLine += nextJump;
                nextJump = 1;
            }

            if (currentLine > endAtLine)
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
}
