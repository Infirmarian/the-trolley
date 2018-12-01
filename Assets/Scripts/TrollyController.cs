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
	}
}
