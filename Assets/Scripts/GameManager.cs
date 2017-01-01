using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Vector3 levelStartPosition;
    public Vector3 checkPointPosition;

    public float lowestY = -6.0f;

    public GameObject player;
    public string nextScene;

    public delegate void PlayerDeath();
    public static event PlayerDeath onPlayerDeath;

    // Use this for initialization
    void Start () {
        levelStartPosition = player.transform.position;
        checkPointPosition = levelStartPosition;
        backToCheckpoint();
    }
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
        if (player.transform.position.y <= lowestY) {
            playerDeath();
        }
	}



    public void backToCheckpoint() {
        player.transform.position = checkPointPosition;
    }

    public void playerDeath() {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (onPlayerDeath != null) {
            onPlayerDeath();
        }
        backToCheckpoint();
    }

    public void nextLevel() {
        SceneManager.LoadScene(nextScene);
    }
}
