using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ScrollMover : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        //check every frame because GameController might speed up the game!
        rb.velocity = new Vector3(0, 0, -1f * GameController.instance.moveRate);
    }
}
