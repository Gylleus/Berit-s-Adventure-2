using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlock : MonoBehaviour {

    public float rotationSpeed = 1.0f;
    public bool childLocked;

    public GameObject rotatingChild;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 rot = transform.eulerAngles;
        rot.z += rotationSpeed;
        transform.eulerAngles = rot;
        if (childLocked && rotatingChild != null) {
            rot.z = -rot.z;
            rotatingChild.transform.localEulerAngles = rot;
        }
	}
}
