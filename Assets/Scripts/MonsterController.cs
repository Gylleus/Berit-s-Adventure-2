using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public GameObject parentPlatform;
    public bool spriteFacingLeft = true;

    private Bounds platformBounds;
    private Rigidbody2D rigid;
    private SpriteRenderer rend;

    public float movementSpeed = 1.0f;
    private int direction = 1;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        platformBounds = parentPlatform.GetComponent<BoxCollider2D>().bounds;
        setDirection();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        checkTurn();
        rigid.velocity = new Vector2(movementSpeed * direction, 0);
    }

    private void setDirection() {
        rend.flipX = direction == -1;
        if (spriteFacingLeft) {
            rend.flipX = !rend.flipX;
        }
    }

    private void checkTurn() {
        if (transform.position.x + rigid.velocity.x * Time.deltaTime > platformBounds.max.x && direction == 1) {
            direction = -1;
            setDirection();
        } else if (transform.position.x + rigid.velocity.x * Time.deltaTime < platformBounds.min.x && direction == -1) {
            direction = 1;
            setDirection();
        }
    }
}
