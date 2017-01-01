using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private Animator anim;
    public GameObject gameManager;

    public float spawnHeightAbove = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Player") {
            activateCheckpoint();
        }
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        if (gameManager == null) {
            gameManager = GameObject.Find("Game Manager");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void activateCheckpoint() {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject c in checkpoints) {
            if (c != gameObject) {
                c.GetComponent<Checkpoint>().deactivateCheckpoint();
            }
        }
        anim.SetBool("active", true);
        gameManager.GetComponent<GameManager>().checkPointPosition = transform.position + new Vector3(0, spawnHeightAbove, 0);
    }

    public void deactivateCheckpoint() {
        anim.SetBool("active", false);
    }

}
