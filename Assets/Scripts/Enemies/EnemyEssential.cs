using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEssential : MonoBehaviour {

    private Vector3 startPos;
    private Rigidbody2D rigid;
    private MonsterController controller;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        controller = GetComponent<MonsterController>();
	}

    public void OnEnable() {
        GameManager.onPlayerDeath += resetEnemy;
    }

    public void OnDisable() {
        GameManager.onPlayerDeath -= resetEnemy;
    }

    public void resetEnemy() {
        transform.position = startPos;
        if (rigid != null) {
            rigid.velocity = Vector2.zero;
        }
        if (controller != null) {
            controller.direction = controller.getStartDirection();
            controller.setDirection();
        }
    }

}
