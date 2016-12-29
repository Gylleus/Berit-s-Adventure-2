using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    private Rigidbody2D rigid;

    public float verticalSpeed, horizontalSpeed;

    public float maxY, minY, maxX, minX;

    private int horizontalDirection = 1;
    private int verticalDirection = 1;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
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
