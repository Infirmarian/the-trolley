using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollyController : MonoBehaviour {


    Vector3 initPos;

	// Use this for initialization
	void Start () {
        initPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        x = Mathf.Round(x);
        this.transform.position = initPos + new Vector3(x, 0, 0);
        //Slooooow down!
        if (Input.GetKey(KeyCode.Space)) {
            Time.timeScale = 0.5f;
        } else
            Time.timeScale = 1f;
	}
}
