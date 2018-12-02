using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollOverController : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("person")) {
            GameController.instance.SmooshPerson();
            // add stuff here!
            Destroy(other.gameObject);
            
        }
    }
}
