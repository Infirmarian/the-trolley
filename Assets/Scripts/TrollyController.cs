using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollyController : MonoBehaviour {



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //Slooooow down!
        if (Input.GetKey(KeyCode.Space)) {
            Time.timeScale = 0.5f;
        } else
            Time.timeScale = 1f;
	}
}
