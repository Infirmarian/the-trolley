using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlant : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.z < 0) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Random.Range(65f, 67f));

        }
	}
}
