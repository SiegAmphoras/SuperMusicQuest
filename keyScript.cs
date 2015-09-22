using UnityEngine;
using System.Collections;

public class keyScript : MonoBehaviour {
	private playerController pC = null;
	
	
	// Update is called once per frame
	void Update () {
		if(pC == null) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
		pC = player.GetComponent<playerController> ();
		}
	}
	//player picked up a key, remove it, put in inventory
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Player")) {
			pC.keys++;
			GameObject.Destroy (this.gameObject);

		}
	}

}
