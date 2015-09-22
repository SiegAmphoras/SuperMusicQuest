using UnityEngine;
using System.Collections;
using System.Linq;

public class activateChest : MonoBehaviour {

	bool activated = false;
	public string cameraTarget = "";

	void OnTriggerEnter2D(Collider2D other) {
		
		if(other.gameObject.CompareTag("Weapon") ){
			activated = true;
			cameraScript.cam.cameraTarget = cameraTarget;
			GameControl.control.cameraTarget = cameraTarget;
			cameraScript.cam.newTarget = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/CrystalSwitchRed");
		}
	}
}