using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.z < -30f) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 100);
        }
	}
}
