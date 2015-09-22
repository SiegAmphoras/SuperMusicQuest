using UnityEngine;
using System.Collections;
using System.Linq;

public class activateChest2 : MonoBehaviour {
	
	private bool activated = false;
	public GameObject chest;
	
	void OnTriggerEnter2D(Collider2D other) {
		
		if(other.gameObject.CompareTag("Weapon") && !activated){
			activated = true;
			chest.SetActive(true);
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/CrystalSwitchRed");
		}
	}
}