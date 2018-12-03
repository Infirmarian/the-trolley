using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour {

    public GameObject straightTrack;
    public GameObject joinCenter;
    public GameObject joinLeft;
    public GameObject joinRight;
    public GameObject person;
    public Transform pool;

    private List<int[]> track;
    private List<GameObject> objectPool;
    private int currentIndex;
    
	// Use this for initialization
	void Start () {
        track = new List<int[]>();
        objectPool = new List<GameObject>();
        int numTracks = 1;
        int minCount = 15;

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

                minCount = 6;
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
                    GameObject newObj = Instantiate(straightTrack, new Vector3(k - 1, 0.05f, z), Quaternion.identity);
                    newObj.transform.SetParent(this.gameObject.transform);
                }
            }
            // switch, need to instantiate different tracks
            if(track[z][0] == 2) {
                if(track[z-1][0] == 1 && track[z-1][2] == 1) {
                    GameObject newObj = Instantiate(joinCenter, new Vector3(0, 0.05f, z), Quaternion.identity);
                    newObj.transform.SetParent(this.gameObject.transform);

                }
                if(track[z+1][0]==1 && track[z+1][2] == 1) {
                    GameObject newObj = Instantiate(joinCenter, new Vector3(0, 0.05f, z), Quaternion.Euler(0, 180, 0));
                    newObj.transform.SetParent(this.gameObject.transform);
                }
                
                if(track[z+1][0]==1 && track[z - 1][0] == 1 && track[z-1][1] ==1) {
                    GameObject newObj = Instantiate(joinLeft, new Vector3(-1, 0.05f, z), Quaternion.identity);  
                    newObj.transform.SetParent(this.gameObject.transform);

                }
                if (track[z+1][1] == 1 && track[z-1][1] == 1 && track[z-1][2] == 1) {
                    GameObject newObj = Instantiate(joinLeft, new Vector3(0,0.05f, z), Quaternion.identity);
                    newObj.transform.SetParent(this.gameObject.transform);

                }
                if(track[z+1][0] ==1 && track[z+1][1] ==1 && track[z-1][1] == 1) {
                    GameObject newObj = Instantiate(joinLeft, new Vector3(0, 0.05f, z), Quaternion.Euler(0,180,0));
                    newObj.transform.SetParent(this.gameObject.transform);
                }
                if (track[z + 1][1] == 1 && track[z +1 ][2] == 1 && track[z - 1][2] == 1) {
                    GameObject newObj = Instantiate(joinLeft, new Vector3(1, 0.05f, z), Quaternion.Euler(0, 180, 0));
                    newObj.transform.SetParent(this.gameObject.transform);
                }

                // right joins
                if (track[z + 1][1] == 1 && track[z - 1][0] == 1 && track[z - 1][1] == 1) {
                    GameObject newObj = Instantiate(joinRight, new Vector3(0, 0.05f, z), Quaternion.identity);
                    newObj.transform.SetParent(this.gameObject.transform);

                }
                if (track[z + 1][2] == 1 && track[z - 1][1] == 1 && track[z - 1][2] == 1) {
                    GameObject newObj = Instantiate(joinRight, new Vector3(1, 0.05f, z), Quaternion.identity);
                    newObj.transform.SetParent(this.gameObject.transform);

                }
                if (track[z + 1][0] == 1 && track[z + 1][1] == 1 && track[z - 1][0] == 1) {
                    GameObject newObj = Instantiate(joinRight, new Vector3(-1, 0.05f, z), Quaternion.Euler(0, 180, 0));
                    newObj.transform.SetParent(this.gameObject.transform);
                }
                if (track[z + 1][1] == 1 && track[z + 1][2] == 1 && track[z - 1][1] == 1) {
                    GameObject newObj = Instantiate(joinRight, new Vector3(0, 0.05f, z), Quaternion.Euler(0, 180, 0));
                    newObj.transform.SetParent(this.gameObject.transform);
                }




            }
            // Spawn enemies at the switch in the tracks
            if (track[z][0] == 2 && z + 1 < stop && GameController.Sum(track[z + 1]) == 2) {
                int len = 0;
                int pos = z+1;
                while (pos < track.Count && track[pos][0] != 2) {
                    len++;
                    pos++;
                }
                int lCount = Random.Range(1, len / 2);
                int rCount = Random.Range(1, len / 2);
                Vector3 left;
                Vector3 right;
                if (track[z + 1][0] == 1) {
                    left = new Vector3(-1, .4f, z + 2);
                    if (track[z + 1][1] == 1)
                        right = new Vector3(0, .4f, z + 2);
                    else
                        right = new Vector3(1, .4f, z + 2);
                } else {
                    left = new Vector3(0, .4f, z + 2); 
                    right = new Vector3(1, .4f, z + 2);
                }
                Quaternion rot = Quaternion.identity;

                for (int i = 0; i < lCount; i++) {
                    if (Random.Range(0, 2) == 0)
                        rot = Quaternion.identity;
                    else
                        rot = Quaternion.Euler(0, 180, 0);
                    GameObject temp = Instantiate(person, left, rot);
                    temp.transform.SetParent(this.transform);
                    left += new Vector3(0, 0, Random.Range(1f, 3f));
                }
                for (int i = 0; i < rCount; i++) {
                    if (Random.Range(0, 2) == 0)
                        rot = Quaternion.identity;
                    else
                        rot = Quaternion.Euler(0, 180, 0);
                    GameObject temp = Instantiate(person, right, rot);
                    temp.transform.SetParent(this.transform);
                    right += new Vector3(0, 0, Random.Range(1f, 3f));
                }
            }

        }
    }

    void InstantiateTrack() {
        InstantiateTrack(0, track.Count);
    }
}
