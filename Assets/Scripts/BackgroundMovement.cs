using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour {

    public GameObject sun;

    public float backgroundXPadding;

    private float levelMinX, levelMaxX, levelMaxY, levelMinY;
    private float backgroundX, backgroundY;
    private Vector3 screenLeftBottom, screenRightTop;

    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        findLevelBounds();
    }

    // Update is called once per frame
    void Update() {
        moveBackground();
        if (sun != null) {
            Vector3 sunPos = player.transform.position;
        }
    }

    private void findLevelBounds() {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject p in platforms) {
            Vector3 platformPos = p.transform.position;
            levelMinX = Mathf.Min(platformPos.x, levelMinX);
            levelMaxX = Mathf.Max(platformPos.x, levelMaxX);
            levelMaxY = Mathf.Max(platformPos.y, levelMaxY);
        }
        levelMinY = GameObject.Find("Game Manager").GetComponent<GameManager>().lowestY;
        levelMinX -= backgroundXPadding;
        levelMaxX += backgroundXPadding;
        levelMaxY += player.GetComponent<PlayerController>().jumpingHeight;

        Bounds backgroundBounds = GetComponent<SpriteRenderer>().sprite.bounds;
        backgroundX = 1f * (backgroundBounds.max.x - backgroundBounds.min.x) * transform.localScale.x;
        backgroundY = 1f * (backgroundBounds.max.y - backgroundBounds.min.y) * transform.localScale.y;
    }

    private void moveBackground() {
        Vector3 screenLeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 20));
        Vector3 screenRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 20));

        float screenXSize = screenRightTop.x - screenLeftBottom.x;
        float screenYSize = screenRightTop.y - screenLeftBottom.y;

        float xOffset = (player.transform.position.x - levelMinX) / (levelMaxX - levelMinX);
        float yOffset = (player.transform.position.y - levelMinY) / (levelMaxY - levelMinY);

        float newX = (xOffset * (screenXSize - backgroundX)) - (screenXSize - backgroundX) / 2;
        float newY = (yOffset * (screenYSize - backgroundY)) - (screenYSize - backgroundY) / 2;

        transform.position = player.transform.position + new Vector3(newX, newY, 1);

    }

}
