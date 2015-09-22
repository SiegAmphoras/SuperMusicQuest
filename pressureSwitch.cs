using UnityEngine;
using System.Collections;

public class pressureSwitch : MonoBehaviour {
	bool isPressed;
	//public GameObject  door;
	private changeSceneScript door;
	//public GameObject thedoor;
	//FMOD.Studio.CueInstance cue;
	FMOD_StudioEventEmitter emitter;
	// Use this for initialization
	void Start () {
		isPressed = false;
		door = GameObject.FindGameObjectsWithTag("door")[0].GetComponent<changeSceneScript>();
		emitter = GetComponent<FMOD_StudioEventEmitter> ();
	}
	
	// Update is called once per frame
	void Update () {
			//TODO
			//animate
			//unlock target
			//sound?

	}
	void OnTriggerEnter2D (Collider2D other) {
		isPressed = true;
		door.UnlockDoor ();
		emitter.Play ();
	}
	void OnTriggerExit2D(Collider2D other){
		isPressed = false;
		//door.LockDoor();
		emitter.Play ();
	}
}
