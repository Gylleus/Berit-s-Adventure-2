using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFollower : MonoBehaviour {

    public float distanceFromParent = 0f;

    public GameObject leader;
    private SkullController leaderController;

    public Transform following;
    private Rigidbody2D parentRigid;


	// Use this for initialization
	void Start () {
        parentRigid = following.gameObject.GetComponent<Rigidbody2D>();
        leaderController = leader.GetComponent<SkullController>();
        if (distanceFromParent == 0) {
            distanceFromParent = leaderController.uniformDistance;
        }
        if (leader == null) {
            Debug.LogWarning("No skull leader for follower.");
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 dif = following.position - transform.position;
        if (dif.magnitude > distanceFromParent || leaderController.moveWhenClose) {
            transform.position = following.position - dif.normalized * distanceFromParent;
        }
    }
     
}
