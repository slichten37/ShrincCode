﻿using System;
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

    public bool isReady;
    public ActivateTextAtLine nextBox;

    public AlertMovement alert;

    public bool autoActivate;


    // Use this for initialization
    void Start() {
        theTextBox = FindObjectOfType<TextBoxManager>();
        alert = FindObjectOfType<AlertMovement>();

    }

    // Update is called once per frame
    void Update() {

        if (!isReady)
        {
            return;
        }

        if (waitForButtonPress && Input.GetKeyDown(KeyCode.Return) || autoActivate)
        {

            isReady = false;
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            if (destroyWhenActivated)
            {
                alert.GetComponent<Renderer>().enabled = false;
                if (nextBox != null)
                {
                    nextBox.setIsReady(true);
                }
                Destroy(gameObject);
            }
            else
            {
                isReady = false;
            }

        }



    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!destroyWhenActivated)
        {
            isReady = true;
        }
        if (other.name == "Player")
        {
            if (requireButtonPress)
            {
                if (isReady)
                {
                    alert.GetComponent<Renderer>().enabled = true;
                    waitForButtonPress = true;
                }
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
            nextBox.setIsReady(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            alert.GetComponent<Renderer>().enabled = false;
            waitForButtonPress = false;
        }
    }

    void setIsReady(bool ready)
    {
        isReady = ready;
    }
}
