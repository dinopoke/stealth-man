using UnityEngine;
using System.Collections;

public class ObjectiveController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D (Collider2D col) {
        if ( col.CompareTag("Player")) {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
