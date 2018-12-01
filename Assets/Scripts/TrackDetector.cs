using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackDetector : MonoBehaviour {

    int trackCount = 1;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("track")) {
            trackCount++;
            GameController.instance.CurrentTrackCount(trackCount);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("track")) {
            trackCount--;
            GameController.instance.CurrentTrackCount(trackCount);
        }
    }
}
