using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public float moveRate;
    public float speedIncrease = 0.15f;
    public int bufferCount;
    public int score = 1000;
    public int personValue = 25;
    public Text scoreText;
    public GameObject trolley;
    public Animator fade;
    public Text finalScoreText;
    public GameObject menu, restart;
    [Range(3, 7)]
    public int trackSegmentation;

    private float finalScore;
    private bool gameOver = false;
    private Vector3 initTrolleyPos;
    public static GameController instance;
    private List<int[]> trackList;
    private int currentTrackSpot;
    private int trackCount = 0;
    private bool switched = false;
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
        scoreText.text = "Score: " + score.ToString();
        finalScoreText.text = "";
        menu.SetActive(false);
        restart.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameOver) {
            moveRate += Time.deltaTime * 0.1f;
            finalScore += (Time.deltaTime * moveRate);
        }
        if (score <= 0){
            if (!gameOver) {
                scoreText.text = "";
                GameOver();
            }
        }else
            scoreText.text = "Score: " + score.ToString();
    }
    public bool IsGameOver() {
        return gameOver;
    }

    private void GameOver() {
        gameOver = true;
        fade.SetTrigger("FadeScreen");
        menu.SetActive(true);
        restart.SetActive(true);
        if (!switched) {
            finalScoreText.text = "You win. The trolley must decide for itself";
            finalScoreText.fontSize = 40;
        } else {
            finalScoreText.text = Mathf.RoundToInt(finalScore).ToString();
        }
        StartCoroutine(EndAfterSeconds(5f));
    }
    IEnumerator EndAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        moveRate = 0f;
    }
//TODO: Bugfix
// For some reason, the trolley will seemingly randomly switch tracks or fail to switch tracks,
// resulting in it getting off the rails
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
            if(x != 0 && !gameOver) {
                switched = true;
                if(x == -1 && nextTrack[0] == 1) {
                    MoveTrolleyToPosition(new Vector3(initTrolleyPos.x - 1, initTrolleyPos.y, initTrolleyPos.z));
                }
                if(x == 1 && nextTrack[2] == 1) {
                    MoveTrolleyToPosition(new Vector3(initTrolleyPos.x +1, initTrolleyPos.y, initTrolleyPos.z));
                }
                if(x == 1 && nextTrack[1] == 1 && Mathf.RoundToInt(trolley.transform.position.x) == -1) {
                    MoveTrolleyToPosition(new Vector3(initTrolleyPos.x, initTrolleyPos.y, initTrolleyPos.z));

                }
                if (x == -1 && nextTrack[1] == 1 && Mathf.RoundToInt(trolley.transform.position.x) == 1) {
                    MoveTrolleyToPosition(new Vector3(initTrolleyPos.x, initTrolleyPos.y, initTrolleyPos.z));
                }

            } else {
                // split tracks
                if (nextTrack[1] == 0) {
                    int xpos = (Random.Range(0, 2) * 2) - 1;
                    MoveTrolleyToPosition(new Vector3(initTrolleyPos.x + xpos, initTrolleyPos.y, initTrolleyPos.z));
                }
                // if no input and the tracks don't diverge, stay on the current track
            }
        }

    }

    private void MoveTrolleyToPosition(Vector3 pos) {
        StartCoroutine(AnimateTrolley(pos));
    }

    IEnumerator AnimateTrolley(Vector3 pos) {
        float time = 1 / moveRate;
        Vector3 velocity = Vector3.zero;
        while (Mathf.Abs(trolley.transform.position.x - pos.x) > 0.01) {
            yield return new WaitForEndOfFrame();
            trolley.transform.position = Vector3.SmoothDamp(trolley.transform.position, pos, ref velocity, time);
        }

        trolley.transform.position = pos;
    }



    public static int Sum(int[] arr) {
        int sum = 0;
        for(int i=0; i<arr.Length; i++) {
            sum += arr[i];
        }
        return sum;
    }

    public void SmooshPerson() {
        score -= personValue;
    }

    public void SetListIndex(int i) {
        if (i - 1 > currentTrackSpot) {
            Debug.Log("ERROR, SKIPPED AN INDEX SOMEHOW");
        }
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
