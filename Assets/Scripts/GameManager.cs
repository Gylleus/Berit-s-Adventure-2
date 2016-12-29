using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Vector3 levelStartPosition;
    public Vector3 checkPointPosition;
    public GameObject background;

    public float lowestY = -6.0f;

    public GameObject player;
    public string nextScene;

    private float levelMinX, levelMaxX, levelMaxY, levelMinY;
    private float backgroundX, backgroundY;

	// Use this for initialization
	void Start () {
        levelStartPosition = player.transform.position;
        checkPointPosition = levelStartPosition;
        backToCheckpoint();
        findLevelBounds();
    }
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
        if (player.transform.position.y <= lowestY) {
            playerDeath();
        }
        moveBackground();
	}

    private void findLevelBounds() {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Ground");
        foreach(GameObject p in platforms) {
            Vector3 platformPos = p.transform.position;
            levelMinX = Mathf.Min(platformPos.x, levelMinX);
            levelMaxX = Mathf.Max(platformPos.x, levelMaxX);
            levelMaxY = Mathf.Max(platformPos.y, levelMaxY);
        }
        levelMinY = lowestY;
        levelMaxY += player.GetComponent<PlayerController>().jumpingHeight;

        Bounds backgroundBounds = background.GetComponent<SpriteRenderer>().sprite.bounds;
        backgroundX = 1f * (backgroundBounds.max.x - backgroundBounds.min.x);
        backgroundY = 1f * (backgroundBounds.max.y - backgroundBounds.min.y);
        Debug.Log(backgroundX);
    }

    private void moveBackground() {
        Vector3 screenLeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 20));
        Vector3 screenRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 20));
        float backgroundMinX = levelMinX - (screenRightTop.x - screenLeftBottom.x);
        float backgroundMaxX = levelMaxX + (screenRightTop.x - screenLeftBottom.x);
        float backgroundMinY = levelMinY - (screenRightTop.y - screenLeftBottom.y);
        float backgroundMaxY = levelMaxY + (screenRightTop.y - screenLeftBottom.y);

        float xOffset = (player.transform.position.x - levelMinX) / (levelMaxX - levelMinX);
        float yOffset = (player.transform.position.y - levelMinY) / (levelMaxY - levelMinY);

        background.transform.position = player.transform.position + new Vector3(backgroundX / 2 - xOffset * backgroundX, backgroundY / 2 - yOffset * backgroundY, 1);

    }

    public void backToCheckpoint() {
        player.transform.position = checkPointPosition;
    }

    public void playerDeath() {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        backToCheckpoint();
    }

    public void nextLevel() {
        SceneManager.LoadScene(nextScene);
    }
}
