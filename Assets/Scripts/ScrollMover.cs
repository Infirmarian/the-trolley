using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ScrollMover : MonoBehaviour {

    private Rigidbody rb;
    private float currVel;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        currVel = GameController.instance.moveRate;
        rb.velocity = new Vector3(0, 0, -1f * currVel);
    }
    private void Update() {
        //check every frame because GameController might speed up the game!
        if (currVel < GameController.instance.moveRate) {
            currVel = GameController.instance.moveRate;
            rb.velocity = new Vector3(0, 0, -1f * currVel);
        }
    }
}
