using UnityEngine;
using System.Collections;
using System.Linq;

public class switchActivate : MonoBehaviour {


	public Transform unlockTarget;
	bool activated = false;
	string cameraTarget = "";

	// Use this for initialization
	void Start () {
		cameraTarget = "testChest";
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {

		if(gameObject.Equals(GameObject.Find("Switch1")) && other.gameObject.CompareTag("Weapon") ){
			activated = true;
			cameraScript.cam.cameraTarget = "testChest";
			GameControl.control.cameraTarget = "testChest";
			cameraScript.cam.newTarget = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/CrystalSwitchRed");
			/*changeSceneScript doorScript = unlockTarget.GetComponent<changeSceneScript> ();
			if(PlayerPrefs.GetInt(unlockTarget.name)==1)
				doorScript.LockDoor();
			else {
				doorScript.UnlockDoor();
			}*/
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		/**if(gameObject.Equals(GameObject.Find("Switch1")) && other.gameObject.CompareTag("Weapon") && !activated){
			activated = true;
			cameraScript.cam.cameraTarget = "testChest";
			GameControl.control.cameraTarget = "testChest";
			cameraScript.cam.newTarget = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/CrystalSwitchRed");
		}**/
	}
}