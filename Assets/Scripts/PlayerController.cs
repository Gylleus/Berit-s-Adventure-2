using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject gameManager;

    private Rigidbody2D rigid;
    private SpriteRenderer rend;
    private Animator anim;

    public float movementSpeed = 1.0f;
    public float jumpingHeight = 10.0f;
    public int jumpsInAir = 1;
    private float jumpsLeft = 0;

    private bool facingLeft = false;
    private bool inAir = false;


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Enemy") {
            gameManager.GetComponent<GameManager>().playerDeath();
        } 
    }

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        handleMovement();
        rend.flipX = facingLeft;
        checkInAir();
	}

    private void checkInAir() {

        if (Mathf.Abs(rigid.velocity.y) > 0) {
            inAir = true;
        } else {
            inAir = false;
            jumpsLeft = jumpsInAir;
        }

    }

    private void handleMovement() {

        Vector2 speed = rigid.velocity;
        bool moving = false;
        bool jumping = false;
        speed.x = 0;

        if (Input.GetKey("a")) {
            speed.x = -movementSpeed * Time.deltaTime;
            facingLeft = true;
            moving = true;
        }
        else if (Input.GetKey("d")) {
            speed.x = movementSpeed * Time.deltaTime;
            facingLeft = false;
            moving = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!inAir) {
                speed.y = jumpingHeight;
            } else if (jumpsLeft > 0) {
                speed.y = jumpingHeight;
                jumpsLeft--;
            }
        }

        rigid.velocity = speed;
        anim.SetBool("running", moving);
    }
}
