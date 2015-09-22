using UnityEngine;
using System.Collections;

public class NPCSpeechScript : MonoBehaviour {

	string statement = "";
	bool displayText = false;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("NPC") == gameObject) {
			statement = "Something about this well makes me anxious. I can't stop running...I must....go fast!";
		} else if (GameObject.Find ("NPC2") == gameObject) {
			statement = "It's scary living so far from the village. Fortunately I get a lot of cardio this way too, so I can outrun any monsters!";
		} else if (GameObject.Find ("NPC3") == gameObject) {
			statement = "...................................X gon giv it to ya";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((GameObject.Find ("Player").transform.position.x >= transform.position.x - .5f) && (GameObject.Find ("Player").transform.position.x <= transform.position.x + .5f) 
			&& (GameObject.Find ("Player").transform.position.y <= transform.position.y) && (GameObject.Find ("Player").transform.position.y >= transform.position.y - 1.5f) 
			&& GameControl.control.direction == 1) {
			print("Near NPC");
			GameControl.control.nearNPC = true;
		} else if ((GameObject.Find ("Player").transform.position.x >= transform.position.x - 1f) && (GameObject.Find ("Player").transform.position.x <= transform.position.x) 
			&& (GameObject.Find ("Player").transform.position.y <= transform.position.y + .3f) && (GameObject.Find ("Player").transform.position.y >= transform.position.y - .7f) 
			&& GameControl.control.direction == 2) {
			print("Near NPC");
			GameControl.control.nearNPC = true;
		} else if ((GameObject.Find ("Player").transform.position.x >= transform.position.x - .1f) && (GameObject.Find ("Player").transform.position.x <= transform.position.x + .6f) 
			&& (GameObject.Find ("Player").transform.position.y <= transform.position.y + 1f) && (GameObject.Find ("Player").transform.position.y >= transform.position.y) 
			&& GameControl.control.direction == 3) {
			print("Near NPC");
			GameControl.control.nearNPC = true;
		} else if ((GameObject.Find ("Player").transform.position.x >= transform.position.x) && (GameObject.Find ("Player").transform.position.x <= transform.position.x + 1.5f) 
			&& (GameObject.Find ("Player").transform.position.y <= transform.position.y + .3f) && (GameObject.Find ("Player").transform.position.y >= transform.position.y - .7f) 
			&& GameControl.control.direction == 4) {
			print("Near NPC");
			GameControl.control.nearNPC = true;
		} else {
			GameControl.control.nearNPC = false;
		}

		if(Input.GetKeyDown ("z") && displayText) {
			displayText = false;
			GameControl.control.unpauseGame();
			GameControl.control.nearNPC = false;
		}

		if(Input.GetKeyDown("z") && GameControl.control.nearNPC == true && !GameControl.control.sceneTransitioning){
			print("Should display and pause");
			displayText = true;
			GameControl.control.pauseForScene();
		}
	}

	void OnGUI(){
		if(displayText == true){
			GUI.TextArea(new Rect(425, 175, 200, 100), statement);
		}
	}
}
