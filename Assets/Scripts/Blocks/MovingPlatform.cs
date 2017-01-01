using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    private Rigidbody2D rigid;

    public float verticalSpeed, horizontalSpeed;

    public float yUp, yDown, xRight, xLeft;
    public float maxY, minY, maxX, minX;

    public int horizontalDirection = 1;
    public int verticalDirection = 1;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        maxX = transform.position.x + xRight;
        minX = transform.position.x - xLeft;
        maxY = transform.position.y + yUp;
        minY = transform.position.y - yDown;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        handleMovement();
	}

    private void handleMovement() {
        if (horizontalSpeed > 0) {
            if ((transform.position.x > maxX && horizontalDirection == 1) || (transform.position.x < minX && horizontalDirection == -1)) {
                horizontalDirection *= -1;
            }
        }
        if (verticalSpeed > 0) {
            if ((transform.position.y > maxY && verticalDirection == 1) || (transform.position.y < minY && verticalDirection == -1)) {
                verticalDirection *= -1;
            }
        }
        rigid.AddForce(new Vector2(horizontalSpeed * horizontalDirection, verticalSpeed * verticalDirection));
    }

    

}
