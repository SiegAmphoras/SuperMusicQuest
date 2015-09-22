using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class openChest2 : MonoBehaviour {

	public List<int> chestContents;
	private Inventory inventory;

	void Start() {
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		for(int i  = 0; i < chestContents.Count; i++) {
			inventory.AddItem(chestContents[i]);
		}
		DestroyObject(gameObject);
	}
}
