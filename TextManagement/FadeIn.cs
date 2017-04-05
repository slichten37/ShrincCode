using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    public float minimum = 0.0f;
    public float maximum = 1f;
    public float duration = 5.0f;
    private float startTime;
    public SpriteRenderer sprite;

    public bool isReady;
    public bool timeSet;
    public bool fadedIn;

    public PlayerMovement player;

    public GameObject[] NPCToShow;
    public GameObject[] NPCToHide;

    // Use this for initialization
    void Start()
    {
        sprite = FindObjectOfType<FadeIn>().GetComponent<SpriteRenderer>();
        sprite.color = new Color(1f, 1f, 1f, 0f);
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isReady)
        {
            fadedIn = false;
            timeSet = false;
            return;
        }
        player.canMove = false;
        if (!timeSet)
        {
            startTime = Time.time;
            timeSet = true;
        }
        if (!fadedIn)
        {
            float t = (Time.time - startTime) / duration;
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));


            if (sprite.color.Equals(new Color(1f, 1f, 1f, 1f)))
            {
                fadedIn = true;
                timeSet = false;
            }
        }
        else
        {
            if (!timeSet)
            {
                startTime = Time.time;
                timeSet = true;   
            }
            HideVisibleNPCs();
            ShowHiddenNPCs();
            float t = (Time.time - startTime) / duration;
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(maximum, minimum, t));

            if (sprite.color.Equals(new Color(1f, 1f, 1f, 0f)))
            {
                fadedIn = false;
                timeSet = false;
                isReady = false;
                player.canMove = true;
            }
        }
        
    }

    public void ShowHiddenNPCs() //for game state changes after dialogue
    {
        for (int i = 0; i < NPCToShow.Length; i++)
        {
            NPCToShow[i].GetComponent<Renderer>().enabled = true;
            if (NPCToShow[i].GetComponent<BoxCollider2D>() != null)
            {
                NPCToShow[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void HideVisibleNPCs()
    {
        for (int i = 0; i < NPCToHide.Length; i++)
        {
            NPCToHide[i].GetComponent<Renderer>().enabled = false;
            if (NPCToHide[i].GetComponent<BoxCollider2D>() != null)
            {
                NPCToHide[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void setIsReady(bool ready)
    {
        isReady = ready;
    }
}
