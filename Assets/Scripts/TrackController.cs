using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour {

    public GameObject trackPrefab;

    private List<int[]> track;
    private List<GameObject> trackObjects;
    private int currentIndex;
	// Use this for initialization
	void Start () {
        track = new List<int[]>();
        trackObjects = new List<GameObject>();
        int numTracks = 1;
        int minCount = 10;

        track.Add(new int[] { 0, 1, 0 });
        GenerateTrack(GameController.instance.bufferCount, minCount);
        InstantiateTrack();
        GameController.instance.SetList(ref track);
	}


    void GenerateTrack(int numToGen, int minCount) {
        int final = numToGen + track.Count;
        for (int i = track.Count; i < final; i++) {
            if (Random.Range(0f, 1f) > 0.8f && minCount < 0 && i < final-2) {
                //add transition pattern
                int[] currentPattern = track[i - 1];
                track.Add(new int[] { 2, 2, 2 }); //transition patten
                int currNumTracks = 0;
                for (int k = 0; k < currentPattern.Length; k++) {
                    currNumTracks += currentPattern[k];
                }

                int[] newPattern = { 0, 0, 0 };

                // choose next pattern to fit
                if (currNumTracks == 1) {
                    if (currentPattern[0] == 1) {
                        newPattern[0] = 1;
                        newPattern[1] = 1;
                    } else if (currentPattern[1] == 1) {
                        int rand = Random.Range(0, 3);
                        switch (rand) {
                            case 0:
                            newPattern[0] = 1;
                            newPattern[1] = 1;
                            break;
                            case 1:
                            newPattern[1] = 1;
                            newPattern[2] = 1;
                            break;
                            case 2:
                            newPattern[0] = 1;
                            newPattern[2] = 1;
                            break;
                        }
                    } else {
                        newPattern[1] = 1;
                        newPattern[2] = 1;
                    }
                } else {
                    if (currentPattern[1] == 0) { // X-X case
                        newPattern[1] = 1;
                    } else if (currentPattern[0] == 1) { // XX- case
                        int rand = Random.Range(0, 2);
                        newPattern[rand] = 1;
                    } else { //-XX case
                        int rand = Random.Range(1, 3);
                        newPattern[rand] = 1;
                    }
                }

                minCount = 5;
                i += 1;
                track.Add(newPattern);
            } else {
                track.Add(new int[] { track[i - 1][0], track[i - 1][1], track[i - 1][2] });
            }
            minCount--;
        }
    }




    private void CycleTracks(int buf) {
        track.RemoveRange(0, buf);
        currentIndex = 0;
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        foreach (Transform child in transform) {
            if (child.position.z < 0) {
                Destroy(child.gameObject);
            } else {
                child.position = new Vector3(child.position.x, child.position.y, child.position.z - 20);
            }
        }
        GenerateTrack(buf, 2);
        InstantiateTrack(track.Count - buf, track.Count);
    }


    
    private void Update() {
        currentIndex = -(int)this.transform.position.z;
        if(currentIndex >= 20) {
            CycleTracks(20);
        }
        GameController.instance.SetListIndex(currentIndex);
    }

    void InstantiateTrack(int start, int stop) {
        int z = start;
        for (; z < stop; z++) {
            for (int k = 0; k < track[z].Length; k++) {
                if (track[z][k] == 1) {
                    GameObject newObj = Instantiate(trackPrefab, new Vector3(k - 1, 0.05f, z), Quaternion.identity);
                    newObj.transform.SetParent(this.gameObject.transform);
                }
            }
        }
    }

    void InstantiateTrack() {
        InstantiateTrack(0, track.Count);
    }
}
