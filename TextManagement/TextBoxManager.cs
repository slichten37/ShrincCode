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

    public ActivateTextAtLine option1;
    public ActivateTextAtLine option2;

    public bool inOptions;

    String[] optionStarters;
    int[] optionTrackers;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        inOptions = false;
        optionStarters = new String[] { "Z: ", "X: ", "C: ", "V: " };
        optionTrackers = new int[4];

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
        else if (textLines[currentLine].Contains("%"))
        {
            if (!inOptions)
            {
                theText.text = textLines[currentLine].Substring(3, textLines[currentLine].Length - 9);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    inOptions = true;
                }
            }
            else
            {
                int options = int.Parse(textLines[currentLine].Substring(textLines[currentLine].Length - 2, 1));
                theText.text = "";
                int count = 0;
                int optionsLeft = 0;
                for (int i = 1; i <= options; i++)
                {
                    if (textLines[currentLine + i].Contains("%")) {
                        optionsLeft++;
                    }
                }

                if(optionsLeft == 0)
                {
                    int x = textLines[currentLine].Length;
                    next = int.Parse(textLines[currentLine].Substring(x - 6, 3));
                    setNext(next);
                }
                else
                {
                    for (int i = 1; i <= options; i++)
                    {
                        if (textLines[currentLine + i].Contains("%"))
                        {
                            theText.text = theText.text + optionStarters[count] + textLines[currentLine + i].Substring(3, textLines[currentLine + i].Length - 8) + "\n";
                            optionTrackers[count] = i;
                            count++;
                            if (count == 4)
                            {
                                i = options + 1;
                            }
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    int a = optionTrackers[0]; //helps find which line to parse for nextLine
                    int x = textLines[currentLine + a].Length;
                    next = int.Parse(textLines[currentLine + a].Substring(x - 5, 3));
                    textLines[currentLine + a] = textLines[currentLine + a].Remove(x - 2);
                    inOptions = false;
                    setNext(next);

                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (optionsLeft < 2)
                    {
                        //do nothing
                    }
                    else
                    {
                        int a = optionTrackers[1];
                        int x = textLines[currentLine + a].Length;
                        next = int.Parse(textLines[currentLine + a].Substring(x - 5, 3));
                        textLines[currentLine + a] = textLines[currentLine + a].Remove(x - 2);
                        inOptions = false;
                        setNext(next);
                    }
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (optionsLeft < 3)
                    {
                        //do nothing
                    }
                    else
                    {
                        int a = optionTrackers[2];
                        int x = textLines[currentLine + a].Length;
                        next = int.Parse(textLines[currentLine + a].Substring(x - 5, 3));
                        textLines[currentLine + a] = textLines[currentLine + a].Remove(x - 2);
                        inOptions = false;
                        setNext(next);
                    }
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (optionsLeft < 4)
                    {
                        //do nothing
                    }
                    else
                    {
                        int a = optionTrackers[3];
                        int x = textLines[currentLine + a].Length;
                        next = int.Parse(textLines[currentLine + a].Substring(x - 5, 3));
                        textLines[currentLine + a] = textLines[currentLine + a].Remove(x - 2);
                        inOptions = false;
                        setNext(next);
                    }
                }
            }
        }
        else if (textLines[currentLine].Contains("~"))
        {
            theText.text = textLines[currentLine].Substring(3, textLines[currentLine].Length - 8);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (textLines[currentLine].Contains("~~1"))
                {
                    option1.setIsReady(true);
                    option1.ShowHiddenNPCs();
                    option1.HideVisibleNPCs();
                }
                else if (textLines[currentLine].Contains("~~2"))
                { 
                    option2.setIsReady(true);
                    option2.ShowHiddenNPCs();
                    option2.HideVisibleNPCs();
                }
                DisableTextBox();
            }
        }
        else
        {
            theText.text = textLines[currentLine].Substring(3, textLines[currentLine].Length - 3);
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

    public void setNext(int x) //changes currentLine, changes which text is displayed.
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
