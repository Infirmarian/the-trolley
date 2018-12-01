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
        for(int i = 0; i<GameController.instance.bufferCount+10; i++) {
            if (Random.Range(0f, 1f) > 0.8f && minCount < 0) {
                //add transition pattern
            }
            minCount--;
        }
        InstantiateTrack();
	}

    void InstantiateTrack() {
        int z = 0;
        for(;z<track.Count; z++) {
            for(int k = 0; k<track[z].Length; k++) {
                if(track[z][k] == 1) {
                    trackObjects.Add(Instantiate(trackPrefab, new Vector3(k-1, 0.05f, z), Quaternion.identity));
                }
            }
        }
    }
}
