using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Vector3 levelStartPosition;
    public Vector3 checkPointPosition;

    public float lowestY = -6.0f;

    public GameObject player;

	// Use this for initialization
	void Start () {
        checkPointPosition = levelStartPosition;
        backToCheckpoint();
    }
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = new Vector3(player.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        if (player.transform.position.y <= lowestY) {
            playerDeath();
        }
	}

    public void backToCheckpoint() {
        player.transform.position = checkPointPosition;
    }

    public void playerDeath() {
        backToCheckpoint();
    }
}
