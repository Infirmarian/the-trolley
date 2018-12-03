using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpeed : MonoBehaviour {
    private AudioSource audio;
    bool fading = false;
    private void Start() {
        audio = GetComponent<AudioSource>();
    }
    private void Update() {
        if(!GameController.instance.IsGameOver())
        audio.pitch = Mathf.Clamp(1 + (GameController.instance.moveRate / 25f), 1, 3);
        else if (!fading){
            StartCoroutine(Fade(5));
        }
    }
    IEnumerator Fade(float seconds) {
        while(audio.volume > 0.01f) {
            audio.volume -= 0.01f*Time.deltaTime / seconds;
            yield return new WaitForEndOfFrame();
        }
        audio.Stop();
    }
}
