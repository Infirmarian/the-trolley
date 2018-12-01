using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour {

    public GameObject trackPrefab;

    private List<int[]> track;
    private List<GameObject> trackObjects;
	// Use this for initialization
	void Start () {
        track = new List<int[]>();
        trackObjects = new List<GameObject>();
        int numTracks = 1;
        int minCount = 10;

        track.Add(new int[] { 0, 1, 0 });

        for(int i = 1; i<GameController.instance.bufferCount+10; i++) {
            if (Random.Range(0f, 1f) > 0.8f && minCount < 0 && i < GameController.instance.bufferCount+8) {
                //add transition pattern
                int[] currentPattern = track[i-1];
                track.Add(new int[] { 2, 2, 2 }); //transition patten
                int currNumTracks = 0;
                for(int k = 0; k<currentPattern.Length; k++) {
                    currNumTracks += currentPattern[k];
                }

                int[] newPattern = { 0, 0, 0 };

                // choose next pattern to fit
                if(currNumTracks == 1) {
                    if(currentPattern[0] == 1) {
                        newPattern[0] = 1;
                        newPattern[1] = 1;
                    }else if(currentPattern[1] == 1) {
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
                    if(currentPattern[1] == 0) { // X-X case
                        newPattern[1] = 1;
                    }else if(currentPattern[0] == 1) { // XX- case
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
                track.Add(new int[] { track[i-1][0], track[i-1][1], track[i-1][2] });
            }
            minCount--;
        }
        InstantiateTrack();
        GameController.instance.SetList(ref track);
	}

    
    private void Update() {
        GameController.instance.SetListIndex(-(int)this.transform.position.z);
    }


    void InstantiateTrack() {
        int z = 0;
        for(;z<track.Count; z++) {
            for(int k = 0; k<track[z].Length; k++) {
                if(track[z][k] == 1) {
                    GameObject newObj = Instantiate(trackPrefab, new Vector3(k-1, 0.05f, z), Quaternion.identity);
                    newObj.transform.SetParent(this.gameObject.transform);
                }
            }
        }
    }
}
