using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	public bool newTarget = false;
	public bool targetReached = false;
	public bool access = true;
	public string cameraTarget = "";
	public Vector3 targetPosition;
	public static cameraScript cam;


	void Awake () {
		if (cam == null) {
			cam = this;
		} 
	}


	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if(newTarget && !targetReached){
			if(access){
				//60 approx. 1 second
				GameControl.control.pauseForScene();
				access = false;
			}
			float step = 8 * Time.fixedDeltaTime;
			transform.position = Vector3.MoveTowards(GameObject.Find("Main Camera").transform.position, GameObject.Find(cameraTarget).transform.position, step);
			transform.position = new Vector3(transform.position.x, transform.position.y, -10);
			targetPosition = GameObject.Find(cameraTarget).transform.position;

		}
		if ((transform.position.x == targetPosition.x || (transform.position.x - targetPosition.x <= .5f)) 
		    && (transform.position.y == targetPosition.y || (transform.position.y - targetPosition.y <= .5f)) && newTarget) {
			targetReached = true;
			newTarget = false;
			//if("this is a given object") goes here to define different camera movements in the future 
			GameObject.Find(cameraTarget).AddComponent<BoxCollider2D>();
			BoxCollider2D b = GameObject.Find(cameraTarget).GetComponent<BoxCollider2D>();
			b.size = new Vector2(.3f, .23f);
			GameObject.Find(cameraTarget).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/ALttP_Treasure_Chest_2");
			//Use the following(or similar code if I'm wrong) if you want to play an animation before allowing the camera to return to the player
			//GameObject.Find(cameraTarget).GetComponent<Animator>().StartRecording(0);
			//while(GameObject.Find(cameraTarget).GetComponent<Animator>().GetCurrentAnimatorStateInfo().normalizedTime - 1f != 0){
		}
		if (targetReached) {
			float step = 8 * Time.fixedDeltaTime;
			transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, step);
			transform.position = new Vector3(transform.position.x, transform.position.y, -10);
		}
		
		if((transform.position.x == GameObject.Find("Player").transform.position.x || (transform.position.x - GameObject.Find("Player").transform.position.x <= .25f)) 
		   && (transform.position.y == GameObject.Find("Player").transform.position.y || ((transform.position.y - GameObject.Find("Player").transform.position.y) <= .25f)) 
		   && targetReached){
			transform.position = GameObject.Find("Player").transform.position;
			transform.position = new Vector3(transform.position.x, transform.position.y, -10);
			access = true;
			targetReached = false;

			GameControl.control.unpauseGame();
		}
	}



}
