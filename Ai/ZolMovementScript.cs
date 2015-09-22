using UnityEngine;
using System.Collections;

public class ZolMovementScript : MonoBehaviour {
	// Use this for initialization
	public bool stunned = false;
	bool left = false;
	bool right = true;
	public int stunTimer = 0;
	public ZolMovementScript random;
	void Start () {
		if (random == null) {
			random = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(stunned && !GameControl.control.sceneTransitioning) {
			stunTimer--;
			gameObject.GetComponent<Animator>().enabled = false;
			rigidbody2D.velocity = new Vector3(0,0,0);
		}
		if(stunTimer <= 0 && stunned){
			gameObject.GetComponent<Animator>().enabled = true;
			stunned = false;
			left = false;
			right = true;
		}
	}

	void FixedUpdate(){
		Vector3 p = GameObject.Find("Player").transform.position - transform.position ;
		if(!GameControl.control.sceneTransitioning && !stunned) {
			p.Normalize();
			rigidbody2D.velocity = p;
		}
		else{
			rigidbody2D.velocity = new Vector3(0,0,0);
		}

		if(stunTimer <= 30 && stunned) {
			if(left){
				left = false;
				right = true;
				transform.position = new Vector3(transform.position.x - .05f, transform.position.y, 0);
			}
			else if(right){
				right = false;
				left = true;
				transform.position = new Vector3(transform.position.x + .05f, transform.position.y, 0);
			}
		}
	}
}
