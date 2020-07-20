using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManController : MonoBehaviour {

    public float sightRange, angrySpeed, ySight;

    private float initSpeed;
    private MonsterController controller;
    private GameObject player;
    private Animator anim;
    private AudioSource laugh;

	// Use this for initialization
	void Start () {
        controller = GetComponent<MonsterController>();
        initSpeed = controller.movementSpeed;
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        laugh = GetComponent<AudioSource>();
        if (initSpeed == 0) {
            anim.speed = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        float xPosDif = player.transform.position.x - transform.position.x;
        float yPosDif = player.transform.position.y - transform.position.y;
        if (controller.movementSpeed == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Walking")) {
            anim.speed = 0;
        }
        if (Mathf.Abs(xPosDif) <= sightRange && xPosDif/Mathf.Abs(xPosDif) == controller.direction && Mathf.Abs(yPosDif) < ySight) {
            if (!anim.GetBool("angry") && !laugh.isPlaying) {
                laugh.Play();
            }
            anim.SetBool("angry", true);
            controller.movementSpeed = angrySpeed;
            anim.speed = 1f;
        } else {
            anim.SetBool("angry", false);
            controller.movementSpeed = initSpeed;
        }
	}
}
