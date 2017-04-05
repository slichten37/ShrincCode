using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        float newX = player.position.x;
        float newY = player.position.y;
        if (newY > 4)
        {
            newY = 4f;
        }
        if (newY < -12.5)
        {
            newY = -12.5f;
        }
        if (newX > -24.5)
        {
            newX = -24.5f;
        }
        if (newX < -47)
        {
            newX = -47f;
        }
        transform.position = new Vector3(newX, newY, -10); // Camera follows the player but 6 to the right
    }
}
