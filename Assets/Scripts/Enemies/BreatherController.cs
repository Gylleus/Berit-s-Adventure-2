using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatherController : MonoBehaviour {

    public float fireInterval = 1.0f;
    public float fireDuration = 1.0f;
    public float startDelay = 1.0f;

    private float intervalCounter, durationCounter;

    private Animator anim;
    private Rigidbody2D rigid;
    private GameObject fireObject;

    private bool horizontalMoving;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        fireObject = transform.Find("Fire").gameObject;
        fireObject.SetActive(false);
        horizontalMoving = GetComponent<MovingPlatform>().horizontalSpeed > 0;

        startDelay = Mathf.Min(startDelay, fireInterval);
        intervalCounter = startDelay;
        durationCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (horizontalMoving) {
            directionRotate();
        }
        shootFire();
	}

    private void directionRotate() {
        if (rigid.velocity.x >= 0) {
            Vector3 rot = transform.eulerAngles;
            rot.y = 180;
            transform.eulerAngles = rot;
        } else if (rigid.velocity.x <= 0) {
            Vector3 rot = transform.eulerAngles;
            rot.y = 0;
            transform.eulerAngles = rot;
        }
    }

    private void shootFire() {
        if (anim.GetBool("isFiring")) {
            durationCounter -= Time.deltaTime;
            if (durationCounter <= 0) {
                fireObject.SetActive(false);
                intervalCounter = fireInterval;
                anim.SetBool("isFiring", false);
            }
        } else {
            intervalCounter -= Time.deltaTime;
            if (intervalCounter <= 0) {
                durationCounter = fireDuration;
                anim.SetBool("isFiring", true);
                fireObject.SetActive(true);
            }
        }
    }
}
