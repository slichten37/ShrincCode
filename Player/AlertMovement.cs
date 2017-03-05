using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertMovement : MonoBehaviour {

    public bool active = false;

    public Transform player;

    void Update()
    {

        transform.position = new Vector3(player.position.x, player.position.y + .8f, 0); // Camera follows the player but 6 to the right
  
    }

    public void SetActive(bool b)
    {
        active = b;
    }


}
