using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    public Text coinText, deathText, timeText;

    private PlayerController player;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        deathText.text = "x " + gameManager.playerDeaths();
        coinText.text = "" + player.playerCoins() + " / " + gameManager.coinsInLevel();
        setTime();
	}

    private void setTime() {
        float time = Time.time;
        if (time / 60 < 1) {
            timeText.text = "" + Mathf.FloorToInt(time);
        } else {
            timeText.text = "" + Mathf.FloorToInt(time / 60) + ":" + Mathf.FloorToInt(time % 60);
        }
    }

}
