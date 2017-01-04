using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public float rotationSpeed;
    public float rotationRange;

    private int rotationDirection = 1;
    private float initRot;

    private bool hidden = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.name == "Player" && !hidden) {
            other.GetComponent<PlayerController>().pickUpCoin();
            hidden = true;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject,audio.clip.length);
        }
    }

    // Use this for initialization
    void Start () {
        initRot = transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 rot = transform.eulerAngles;

        if (rot.y >= initRot) {
            rotationDirection = -1;
        } else if (rot.y <= initRot - rotationRange) {
            rotationDirection = 1;
        }

        rot.y += rotationSpeed * rotationDirection;
        transform.eulerAngles = rot;
	}
}
