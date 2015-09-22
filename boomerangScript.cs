using UnityEngine;
using System.Collections;

public class boomerangScript : MonoBehaviour {
	bool returningToPlayer = false;
	bool thrown = false;
	bool isSelected = false;
	bool once = true;
	int directionSet = 0;
	int thrownTimer = 0;
	// Use this for initialization
	void Start () {
		isSelected = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if(!returningToPlayer && !thrown){
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			gameObject.GetComponent<SpriteRenderer>().sprite = null;
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
		}
		
		if(isSelected && Input.GetKeyDown("x") && once){
			thrown = true;
			if((GameControl.control.direction == 0 || GameControl.control.direction == 1 || GameControl.control.direction == 7) && once){
				directionSet = 1;
				transform.position = new Vector3(player.transform.position.x, player.transform.position.y + .75f, player.transform.position.z);
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				once = false;
			}
			else if(GameControl.control.direction == 2 && once){
				transform.position = new Vector3(player.transform.position.x + .75f, player.transform.position.y, player.transform.position.z);
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				directionSet = 2;
				once = false;
				
			}
			else if((GameControl.control.direction == 3 || GameControl.control.direction == 4 || GameControl.control.direction == 5) && once){
				transform.position = new Vector3(player.transform.position.x, player.transform.position.y - .75f, player.transform.position.z);
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				directionSet = 3;
				once = false;
				
			}
			else if(GameControl.control.direction == 6 && once){
				transform.position = new Vector3(player.transform.position.x - .75f, player.transform.position.y, player.transform.position.z);
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				directionSet = 4;
				once = false;
				
			}
		}

		if(thrown) {
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/BoomerangALttP");
			thrownTimer++;
			if(thrownTimer >= 45){
				rigidbody2D.velocity = new Vector3(0,0,0);
				thrownTimer = 0;
				thrown = false;
				returningToPlayer = true;
			}
			if(directionSet == 1){
				rigidbody2D.velocity = new Vector3(0, 8f, 0);
				transform.Rotate(new Vector3(0, 0, transform.rotation.z + 20));
			}
			if(directionSet == 2){
				rigidbody2D.velocity = new Vector3(8f, 0, 0);
				transform.Rotate(new Vector3(0, 0, transform.rotation.z + 20));
			}
			if(directionSet == 3){
				rigidbody2D.velocity = new Vector3(0, - 8f, 0);
				transform.Rotate(new Vector3(0, 0, transform.rotation.z + 20));
			}
			if(directionSet == 4){
				rigidbody2D.velocity = new Vector3(- 8f, 0, 0);
				transform.Rotate(new Vector3(0, 0, transform.rotation.z + 20));
			}
		}

		if(returningToPlayer) {
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			Vector3 p = player.transform.position - transform.position ;
			p.Normalize();
			rigidbody2D.velocity = p * 12;
			transform.Rotate(new Vector3(0, 0, transform.localRotation.z + 20));
		}

		if(returningToPlayer && Vector3.Distance(player.transform.position, transform.position) <= .6f) {
			returningToPlayer = false;
			directionSet = 0;
			once = true;
			thrownTimer = 0;
			transform.localRotation.SetFromToRotation(new Vector3(0, 0, transform.rotation.z), new Vector3(0,0,0));
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Enemy")) {
			if(other.gameObject.Equals(GameObject.Find("BZol"))){
				other.gameObject.GetComponent<WanderFollow>().random.stunned = true;
				other.gameObject.GetComponent<WanderFollow>().random.stunTimer = 150;
				thrown = false;
				returningToPlayer = true;
			}
			else if(other.gameObject.Equals(GameObject.Find("GZol"))){
				other.gameObject.GetComponent<RandomAIMovement>().random.stunned = true;
				other.gameObject.GetComponent<RandomAIMovement>().random.stunTimer = 150;
				thrown = false;
				returningToPlayer = true;
			}
			else if(other.gameObject.Equals(GameObject.Find("RZol"))){
				other.gameObject.GetComponent<ZolMovementScript>().random.stunned = true;
				other.gameObject.GetComponent<ZolMovementScript>().random.stunTimer = 150;
				thrown = false;
				returningToPlayer = true;
			}
			else{
				//Enemy can't be stunned
			}
		}
		else if(other.gameObject.Equals(GameObject.Find("Player"))){

		}
		else{
			if(thrown){
				thrown = false;
				returningToPlayer = true;
			}
		}
	}
}
