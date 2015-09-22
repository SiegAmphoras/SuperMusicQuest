using UnityEngine;
using System.Collections;

public class swordScript : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Enemy")){
			other.GetComponent<aiHealth>().takeDamage(1);
			//Destroy (other.gameObject);
		}
	}

	void Awake() {

	}
}
