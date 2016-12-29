using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject.Find("Game Manager").GetComponent<GameManager>().nextLevel();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
