using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Direction currentDir;
	Vector2 input;
	bool isMoving = false;
	Vector3 startPos;
	Vector3 endPos;
	float t;

	public float walkSpeed = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!isMoving) {
			input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
			if (Mathf.Abs (input.x) > input.y)
				input.y = 0;
			else
				input.x = 0;

			if (input != Vector2.zero) {
				StartCoroutine (Move (transform));
			}
		}

	}

	public IEnumorator Move(Transform entity)
	{
		isMoving = true;


		isMoving = false;
		yield return 0;
}

enum Direction
{
	North,
	East,
	South,
	West
}
