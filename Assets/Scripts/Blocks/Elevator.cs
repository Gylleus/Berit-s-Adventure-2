using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    private Vector3 startPos;
    public Vector3 targetPos;

    public float movementSpeed;
    private bool movingBack = false;
    private bool toMoveBack = false;
    private Rigidbody2D rigid;

    private GameObject player;

    private void OnCollisionEnter2D(Collision2D collision) {
        movingBack = false;
        if (collision.transform.name == "Player") {
            player = collision.gameObject;
            toMoveBack = false;
            rigid.velocity = (targetPos - startPos).normalized * movementSpeed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        toMoveBack = true;
    }

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (movingBack) {
            Vector2 vel;
            if ((startPos-transform.position).magnitude < 0.2f) {
                vel = Vector2.zero;
                movingBack = false;
            } else {
                vel = (startPos - targetPos).normalized * movementSpeed;
            }
            rigid.velocity = vel;
        } else if ((targetPos - transform.position).magnitude < 0.2f) {
            movingBack = toMoveBack;
            rigid.velocity = Vector2.zero;
        }
	}

    void OnEnable() {
        GameManager.onPlayerDeath += resetElevator;
    }


    void OnDisable() {
        GameManager.onPlayerDeath -= resetElevator;
    }

    public void resetElevator() {
        transform.position = startPos;
        rigid.velocity = Vector2.zero;
    }
}
