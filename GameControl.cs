using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public static GameControl control; 	//Object for us to reference

	public float health = 6f;			//Player Health
	public float maxHealth=6f;
	public int direction = 3;
	public bool nearChest = false;
	public bool nearNPC = false;
	public bool sceneTransitioning = false;
	public bool pauseCalled = false;
	public int heartNum = 0;

	public bool healthInit = false;
	//If you just want to pause the game for a given number of seconds without moving the camera
	//set pauseTimer using setter to 60 * numSeconds and then call pauseForScene()
	int pauseTimer = 0;
	public string cameraTarget;

	public HealthScript hs;

	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy(gameObject);
		}
		maxHealth = health;
	}

	public void reset() {
		health = 3;
	}

	public void setHS(HealthScript h) {
		hs = h;
	}

	public void ModifyHealth(int amount) {
		hs.ModifyHealth (amount);
	}

	public void AddHearts(int n) {
		hs.AddHearts (n);
	}

	public void pauseForScene(){
		GameControl.control.sceneTransitioning = true;
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			if(go.GetComponent<Animator>() != null){
				go.GetComponent<Animator>().enabled = false;
			}
		}
	}

	public void unpauseGame(){
		while (pauseTimer > 0) {
			pauseTimer --;
			GameControl.control.pauseCalled = false;
		}
		GameControl.control.sceneTransitioning = false;
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			if(go.GetComponent<Animator>() != null){
				go.GetComponent<Animator>().enabled = true;
			}
		}
	}

	public void setPauseTimer(int time){
		GameControl.control.pauseCalled = true;
		GameControl.control.pauseTimer = time;
	}
}
