using UnityEngine;
using System.Collections;

//place this script on locked doors
public class lockedDoorScript : MonoBehaviour {
	private playerController pC = null;
	// Use this for initialization
	
	void Update() {
		if (pC == null) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			pC = player.GetComponent<playerController> (); 
		}
	}
	//player with key touches door, check and open
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Player")) {
			if(pC.keys>=1){
			pC.keys--;
			GameObject.Destroy (this.gameObject);
			}
		}
	}
}
