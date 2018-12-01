using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float moveRate;
    public int bufferCount;
    public int score = 1000;
    public GameObject trolley;

    private Vector3 initTrolleyPos;
    public static GameController instance;
    private List<int[]> trackList;
    private int currentTrackSpot;
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
		initTrolleyPos = trolley.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (score <= 0) {
            GameOver();
        }
	}

    private void GameOver() {

    }

    private void SwitchTracks() {
        int[] nextTrack = trackList[currentTrackSpot + 2];
        if(Sum(nextTrack) == 1) {

            int xpos = 0;

            for(int i =0; i<nextTrack.Length; i++) {
                if (nextTrack[i] == 1)
                    xpos = i - 1;
            }
            MoveTrolleyToPosition(new Vector3(initTrolleyPos.x + xpos, initTrolleyPos.y, initTrolleyPos.z));
        } else {
            int x = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
            if(x == 0) {
                // split tracks
                if (nextTrack[1] == 0) {
                    int xpos = Random.Range(0, 2) * 2 - 1;
                    MoveTrolleyToPosition(new Vector3(initTrolleyPos.x + xpos, initTrolleyPos.y, initTrolleyPos.z));
                }

            } else {
               
                // if no input and the tracks don't diverge, stay on the current track
            }
            //TODO: Move the trolley apart
        }

    }
    //TODO: animate this to make it smoother
    private void MoveTrolleyToPosition(Vector3 pos) {
        trolley.transform.position = pos;
    }



    private static int Sum(int[] arr) {
        int sum = 0;
        for(int i=0; i<arr.Length; i++) {
            sum += arr[i];
        }
        return sum;
    }

    public void SetListIndex(int i) {
        // encountering a new set of track
        if(i != currentTrackSpot) {
            // transition time bois!
            if(trackList[i][0] == 2) {
                Debug.Log("Encountered a track switch");
                SwitchTracks();
            }
        }
        currentTrackSpot = i;
    }

    public void SetList(ref List<int[]> l) {
        trackList = l;
    }


}
