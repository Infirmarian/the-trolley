using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float moveRate;
    public int bufferCount;
    public int score = 1000;
    public static GameController instance;

    private int trackCount = 0;
    //Singleton protection for GameController
    private void Awake() {
        if(GameController.instance == null) {
            GameController.instance = this;
        }else if(GameController.instance != this) {
            Destroy(this);
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (score <= 0) {
            GameOver();
        }
	}

    private void GameOver() {

    }

    public void CurrentTrackCount(int c) {
        trackCount = c;
        Debug.Log("There are currently " + trackCount + " tracks!");
    }


}
