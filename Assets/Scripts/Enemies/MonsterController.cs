using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    private GameObject parentPlatform;
    public bool spriteFacingLeft = true;

    private Bounds platformBounds;
    private Rigidbody2D rigid;
    private SpriteRenderer rend;

    public float movementSpeed = 1.0f;
    private int startDirection;
    public int direction = 1;
    private float xColliderSize;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Ground") {
            parentPlatform = collision.gameObject;
            platformBounds = parentPlatform.GetComponent<BoxCollider2D>().bounds;
        } else if (collision.transform.tag == "Wall") {
            direction *= -1;
            setDirection();
        }
    }

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        Bounds colliderBounds = GetComponent<Collider2D>().bounds;
        xColliderSize = colliderBounds.max.x - colliderBounds.min.x;
        startDirection = direction;
        setDirection();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (parentPlatform != null) {
            checkTurn();
            rigid.velocity = new Vector2(movementSpeed * direction, rigid.velocity.y);
        }
    }

    public void setDirection() {
        rend.flipX = direction == -1;
        if (spriteFacingLeft) {
            rend.flipX = !rend.flipX;
        }
    }

    private void checkTurn() {
        if (transform.position.x + rigid.velocity.x * Time.deltaTime + xColliderSize * 0.6f > platformBounds.max.x && direction == 1) {
            direction = -1;
            setDirection();
        } else if (transform.position.x + rigid.velocity.x * Time.deltaTime - xColliderSize * 0.6f < platformBounds.min.x && direction == -1) {
            direction = 1;
            setDirection();
        }
    }

    public int getStartDirection() {
        return startDirection;
    }
}
