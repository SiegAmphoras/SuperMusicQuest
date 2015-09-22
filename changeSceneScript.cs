using UnityEngine;
using System.Collections;

public class changeSceneScript : MonoBehaviour
{
	GameObject player;
	public GameObject newScene;
	public GameObject currentScene;
	public float destinationX;
	public float destinationY;
	public bool locked;
	ItemDatabase itemDatabase;
	Inventory inventory;
	public int unlockItem = -1;
	
	void Start ()
	{
		player = GameObject.Find ("Player");
		itemDatabase = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if (!locked || (unlockItem != -1 && inventory.InventoryContains (unlockItem))) {
			newScene.SetActive (true);
			currentScene.SetActive (false);
			player.transform.position = new Vector3 (destinationX, destinationY);
		}
		/*print (transform.name+ ": Lock == " + PlayerPrefs.GetInt(transform.name));
		if (PlayerPrefs.GetInt (transform.name) == 0 || (unlockItem != -1 && inventory.InventoryContains(unlockItem))) {
			newScene.SetActive (true);
			currentScene.SetActive (false);
			player.transform.position = new Vector3 (destinationX, destinationY);
		}*/
	}
	
	public void UnlockDoor ()
	{
		print (transform.name+":"+PlayerPrefs.GetInt(transform.name));
		//TODO:reset values to locked on a new game
		PlayerPrefs.SetInt (transform.name, 1);
	}
	
	public void LockDoor ()
	{
		print (transform.name+":"+PlayerPrefs.GetInt(transform.name));
		PlayerPrefs.SetInt (transform.name, 0);
	}
}
