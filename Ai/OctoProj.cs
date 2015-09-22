using UnityEngine;
using System.Collections;

public class OctoProj : MonoBehaviour {
	private float timer=5;

	void Update(){
		timer-=Time.deltaTime;
		if (timer <= 0) {
			GameObject.Destroy(this.gameObject);		
		}
					

	}
	void OnCollisionEnter2D(Collision2D other){
		GameObject.Destroy (this.gameObject);

	}
}
