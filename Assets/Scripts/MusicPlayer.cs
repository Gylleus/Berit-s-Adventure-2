using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private static MusicPlayer instance = null;
    public static MusicPlayer Instance {
        get { return instance; }
    }

    void Awake() {
        setSongProgress();
    }

    private void setSongProgress() {
        if (instance != null && instance != this) {
            if (instance.GetComponent<AudioSource>().clip.name == GetComponent<AudioSource>().clip.name) {
                Destroy(gameObject);
            } else {
                Destroy(instance.gameObject);
            }
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
