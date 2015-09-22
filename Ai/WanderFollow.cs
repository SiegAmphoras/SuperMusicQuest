using UnityEngine;
using System.Collections;
public class WanderFollow : MonoBehaviour {
	//new direction
	public float timer=5.0f;
	public int stunTimer = 0;
	//how often can cause damage
	public float damageFrequency=1.0f;
	//how often can be damaged
	private Vector2 nTransform = new Vector2(1, 1);
	//private float speed=1f;
	private float distance= 3f;
	public bool stunned = false;
	bool left = false;
	bool right = true;
	public WanderFollow random;
	
	// Use this for initialization
	void Start () {
		if (random == null) {
			random = this;
		} 
		//starts the ai off in a random direction
		ChangeDirection ();
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
		if (!rigidbody2D.fixedAngle) {
			//have the enemy rotate towards the player
			Vector3 dir = GameObject.Find("Player").transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+90;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				}
	}

	void FixedUpdate(){
		timer-= Time.fixedDeltaTime;
		if(timer <= 0 && !GameControl.control.sceneTransitioning && !stunned) {
			ChangeDirection ();
		}
		if(!GameControl.control.sceneTransitioning && !stunned){
			rigidbody2D.velocity = nTransform;
		}
		if(Vector3.Distance(transform.position,GameObject.Find("Player").transform.position) < distance && !GameControl.control.sceneTransitioning && !stunned) {
			Follow ();
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

	//changes the ai's direction
	void ChangeDirection(){
		nTransform = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
		nTransform.Normalize ();
		if (nTransform.x == 0 && nTransform.y == 0)
			ChangeDirection ();
		timer = 5f;
	}
	//ai targets player and follows while in a certain range p
	void Follow(){
		Vector3 p = GameObject.Find("Player").transform.position - transform.position ;
		if(!GameControl.control.sceneTransitioning && !stunned) {
			p.Normalize();
			rigidbody2D.velocity = p * 1.5f;
		}
		else{
			rigidbody2D.velocity = new Vector3(0,0,0);
		}
	}
	//ran into player
	void OnCollisionEnter2D(Collision2D other){
		if(!other.gameObject.Equals(GameObject.Find("Player")) && !GameControl.control.sceneTransitioning && !stunned) {
			ChangeDirection();
		}
		
	}

	void OnCollisionStay2D(Collision2D other){
		if(!other.gameObject.Equals(GameObject.Find("Player")) && !GameControl.control.sceneTransitioning &&!stunned) {
			ChangeDirection();
		}
	}
}