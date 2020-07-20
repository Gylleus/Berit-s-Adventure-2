using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour {


    public float movementSpeed;
    public float uniformDistance;
    public bool moveWhenClose = false;

    public Vector3[] targetPositions;
    public int startTarget = 0;
    private int currentTarget;

    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        resetTarget();
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 dif = targetPositions[currentTarget] - transform.position;
        rigid.AddForce((dif).normalized * movementSpeed);

        if (rigid.velocity.magnitude > movementSpeed) {
            rigid.velocity = rigid.velocity.normalized * movementSpeed;
        }

        if (dif.magnitude <= 0.4f) {
            currentTarget = (currentTarget + 1) % targetPositions.Length;
        }

	}


    public void OnEnable() {
        GameManager.onPlayerDeath += resetTarget;
    }

    public void OnDisable() {
        GameManager.onPlayerDeath -= resetTarget;
    }

    public void resetTarget() {
        currentTarget = startTarget;
    }

}
