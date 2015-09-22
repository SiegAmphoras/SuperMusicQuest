using UnityEngine;
using System.Collections;

public class IceScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.CompareTag("Player")) {
			playerController pc = col.gameObject.GetComponent<playerController>();
			pc.OnIce();
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.CompareTag("Player")) {
			playerController pc = col.gameObject.GetComponent<playerController>();
			pc.OffIce();
		}
	}
}
