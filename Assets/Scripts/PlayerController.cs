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


    private Rigidbody2D currentPlatformRigid;
    private bool facingLeft = false;
    private bool inAir = false;
    private float yMomentum;

    private int coinsPickedUp = 0;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Ground") {
            inAir = false;
            jumpsLeft = jumpsInAir;
            currentPlatformRigid = collision.gameObject.GetComponent<Rigidbody2D>();
        }
        else if (collision.transform.tag == "Enemy") {
            gameManager.GetComponent<GameManager>().playerDeath();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.transform.tag == "Ground" || collision.transform.tag == "Wall") {
            inAir = true;
            currentPlatformRigid = null;
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
        yMomentum = rigid.velocity.y;
	}

    private void handleMovement() {

        Vector2 speed = rigid.velocity;
        bool moving = false;
        bool jumping = false;
        speed.x = 0;

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) {
            speed.x = -movementSpeed * Time.deltaTime;
            facingLeft = true;
            moving = true;
        }
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)) {
            speed.x = movementSpeed * Time.deltaTime;
            facingLeft = false;
            moving = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (!inAir) {
                speed.y = jumpingHeight;
            } else if (jumpsLeft > 0) {
                speed.y = jumpingHeight;
                jumpsLeft--;
            }
        }

        if (currentPlatformRigid != null) {
            speed.x += currentPlatformRigid.velocity.x;
        }
        rigid.velocity = speed;
        anim.SetBool("running", moving);
    }

    public void pickUpCoin() {
        coinsPickedUp++;
    }
}
