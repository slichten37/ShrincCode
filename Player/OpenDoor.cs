using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public GameObject door;
    public GameObject door2;
    public AlertMovement alert;

    public bool waitForButtonPress;



    // Use this for initialization
    void Start()
    {
        alert = FindObjectOfType<AlertMovement>();
        waitForButtonPress = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (waitForButtonPress)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                door.GetComponent<Renderer>().enabled = false;
                door.GetComponent<BoxCollider2D>().enabled = false;
                door2.GetComponent<Renderer>().enabled = false;
                door2.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            waitForButtonPress = true;
            alert.GetComponent<Renderer>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            waitForButtonPress = false;
            alert.GetComponent<Renderer>().enabled = false;
            door.GetComponent<Renderer>().enabled = true;
            door.GetComponent<BoxCollider2D>().enabled = true;
            door2.GetComponent<Renderer>().enabled = true;
            door2.GetComponent<BoxCollider2D>().enabled = true;

        }
    }

}
