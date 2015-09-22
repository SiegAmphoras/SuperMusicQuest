using UnityEngine;
using System.Collections;

public class NPCMoveScript : MonoBehaviour {

	Vector2 []points= new Vector2[7];
	Vector2 nextPoint;
	int currentPoint = 0;
	int maximum = 0;
	int direction = 0;
	bool noMovement = true;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("NPC") == gameObject) {
			maximum = 3;
			points = new Vector2[maximum + 1];

			points [0] = new Vector2 (7.8f, -19.6f);
			points [1] = new Vector2 (11.2f, -19.6f);
			points [2] = new Vector2 (11.2f, -17.2f);
			points [3] = new Vector2 (7.8f, -17.2f);

			currentPoint = maximum;
			noMovement = false;
		} else if (GameObject.Find ("NPC2") == gameObject) {
			maximum = 6;
			points = new Vector2[maximum + 1];

			points [0] = new Vector2 (66.3f, -8f);
			points [1] = new Vector2 (66.3f, -4.5f);
			points [2] = new Vector2 (39.75f, -15.75f);
			points [3] = new Vector2 (39.75f, -55f);
			points [4] = new Vector2 (51.5f, -55.5f);
			points [5] = new Vector2 (39.75f, -15.75f);
			points [6] = new Vector2 (74.5f, -8f);

			currentPoint = maximum;
			noMovement = false;

		} else if (GameObject.Find ("NPC3") == gameObject) {
			points[0] = new Vector2(22.5f, -18f);
		}
		nextPoint = points[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameControl.control.sceneTransitioning && !noMovement) {
			rigidbody2D.drag = 10f;
			Vector2 movVec = new Vector2 (nextPoint.x - transform.position.x, nextPoint.y - transform.position.y);
			movVec.Normalize ();
			rigidbody2D.velocity = movVec * 3;
			if(Mathf.Abs(movVec.x) > Mathf.Abs(movVec.y) && movVec.x > 0){
				gameObject.GetComponent<Animator>().SetInteger("direction", 2);
			}
			else if(Mathf.Abs(movVec.y) > Mathf.Abs(movVec.x) && movVec.y > 0){
				gameObject.GetComponent<Animator>().SetInteger("direction", 1);
			}
			else if(Mathf.Abs(movVec.x) > Mathf.Abs(movVec.y) && movVec.x < 0){
				gameObject.GetComponent<Animator>().SetInteger("direction", 4);
			}
			else if(Mathf.Abs(movVec.y) > Mathf.Abs(movVec.x) && movVec.y < 0){
				gameObject.GetComponent<Animator>().SetInteger("direction", 3);
			}

			if(movVec.x == 0 && movVec.y == 0){
				gameObject.GetComponent<Animator>().SetBool("moving", false);
			}
			else{
				gameObject.GetComponent<Animator>().SetBool("moving", true);
			}
		} else {
			rigidbody2D.drag = 100000000f;
			rigidbody2D.velocity = new Vector2(0,0);
		}

		if((transform.position.x - nextPoint.x <= .2f && transform.position.x - nextPoint.x >= -.1f) 
		   && (transform.position.y - nextPoint.y <= .2f && transform.position.y - nextPoint.y >= -.1f) && !noMovement){
			currentPoint += 1;
			if(currentPoint == maximum + 1){
				currentPoint = 0;
				nextPoint = points[1];
			}
			else if(currentPoint == maximum){
				nextPoint = points[0];
			}
			else if(!(currentPoint > maximum - 1)){
				nextPoint = points[currentPoint + 1];
			}
		}
	}
}
