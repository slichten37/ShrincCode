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

	public float walkSpeed = 20f;
    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite westSprite;
    public Sprite southSprite;

    public bool canMove;

    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {

        if (!canMove)
        {
            return;
        }

		if (!isMoving) {

            input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
			if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
				input.y = 0;
			else
				input.x = 0;

            if (input.x < 0)
            {
                currentDir = Direction.West;
            }
            if (input.x > 0)
            {
                currentDir = Direction.East;
            }
            if (input.y < 0)
            {
                currentDir = Direction.South;
            }
            if (input.y > 0)
            {
                currentDir = Direction.North;
            }

            switch (currentDir)
            {
                case Direction.North:
                    gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
                    break;
                case Direction.South:
                    gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                    break;
                case Direction.East:
                    gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                    break;
                case Direction.West:
                    gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                    break;
            }

            if (input != Vector2.zero) {
                if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) ;
                {
                    transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * walkSpeed*2 * Time.deltaTime, 0f, 0f));
                }
                if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f);
                {
                    transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * walkSpeed*2 * Time.deltaTime, 0f));
                }
            }
		}
	}

	/*public IEnumerator Move(Transform entity)
	{
		isMoving = true;
		startPos = entity.position;
		t = 0;

		endPos = new Vector3 (startPos.x + System.Math.Sign (input.x), startPos.y + System.Math.Sign (input.y), startPos.z);

		while (t < 1f) {

			t += Time.deltaTime * walkSpeed;
			entity.position = Vector3.Lerp(startPos, endPos, t);
			yield return null;
		}

		isMoving = false;
		yield return 0;
	}*/

	enum Direction
	{
		North,
		East,
		South,
		West
	}
}
