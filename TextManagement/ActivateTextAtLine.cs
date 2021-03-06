﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;
    public int startLine;

    public TextBoxManager theTextBox;

    public bool destroyWhenActivated;

    public bool requireButtonPress;
    public bool waitForButtonPress;

    public bool isReady;
    public ActivateTextAtLine nextBox;

    public ActivateTextAtLine option1;
    public ActivateTextAtLine option2;
    public ActivateTextAtLine option3;
    public ActivateTextAtLine option4;
    public ActivateTextAtLine option5;

    public AlertMovement alert;

    public bool autoActivate;

    public GameObject[] NPCToHide;
    public GameObject[] NPCToShow;

    public bool changeTime;
    public bool sendPlayerToOffice;

    public FadeIn TimeWindow;


    // Use this for initialization
    void Start() {
        theTextBox = FindObjectOfType<TextBoxManager>();
        alert = FindObjectOfType<AlertMovement>();
        TimeWindow = FindObjectOfType<FadeIn>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!isReady)
        {
            return;
        }

        if ((waitForButtonPress && Input.GetKeyDown(KeyCode.Return)) || (waitForButtonPress && autoActivate))
        {

            isReady = false;
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = 0;
            
            theTextBox.option1 = option1;
            theTextBox.option2 = option2;
            theTextBox.option3 = option3;
            theTextBox.option4 = option4;
            theTextBox.option5 = option5;

            theTextBox.nextBox = nextBox;
            
            theTextBox.EnableTextBox(); // begin dialogue stage after 'return' button press

            if (destroyWhenActivated) //remove shoutzone
            {
                if (nextBox != null)
                {
                    nextBox.setIsReady(true); // progress game state
                }
                alert.GetComponent<Renderer>().enabled = false;
                Destroy(gameObject);
            }
            else
            {
                isReady = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) //when in the zone, prepare for a 'return' button press, which begins dialogue
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
            theTextBox.currentLine = 0;
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

    public void setIsReady(bool ready)
    {
        isReady = ready;
    }

    public void ShowHiddenNPCs() //for game state changes after dialogue
    {
        if (changeTime)
        {
            return;
        }
        for (int i = 0; i < NPCToShow.Length; i++)
        {
            if (NPCToShow[i].GetComponent<Renderer>() != null)
            {
                NPCToShow[i].GetComponent<Renderer>().enabled = true;
            }
            if (NPCToShow[i].GetComponent<BoxCollider2D>() != null)
            {
                NPCToShow[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void HideVisibleNPCs()
    {
        if (changeTime)
        {
            return;
        }
        for (int i = 0; i < NPCToHide.Length; i++)
        {
            if (NPCToHide[i].GetComponent<Renderer>() != null)
            {
                NPCToHide[i].GetComponent<Renderer>().enabled = false;
            }
            if (NPCToHide[i].GetComponent<BoxCollider2D>() != null)
            {
                NPCToHide[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void Progress()
    {
        if (changeTime)
        {
            TimeWindow.NPCToShow = NPCToShow;
            TimeWindow.NPCToHide = NPCToHide;
            if (sendPlayerToOffice)
            {
                TimeWindow.sendPlayerToOffice = true;
            }
            TimeWindow.setIsReady(true);
        }
        else
        {
            HideVisibleNPCs();
            ShowHiddenNPCs();
        }
    }

}
