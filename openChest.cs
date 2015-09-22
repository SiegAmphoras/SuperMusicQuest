using UnityEngine;
using System.Collections;

public class openChest : MonoBehaviour {

	string chestContents = "";
	public string currentChest = "";
	int spriteLinger = 0;
	bool chestOpened = false;
	Vector3 tempPlayer;

	// Use this for initialization
	void Start () {
		chestContents = "HeartContainer";
		if(gameObject.Equals(GameObject.Find(currentChest))){
			chestContents = "HeartContainer";
			currentChest = "testChest";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if((GameObject.Find("Player").transform.position.x > transform.position.x - .75f) && (GameObject.Find("Player").transform.position.x < transform.position.x + .7f) 
		   && (GameObject.Find("Player").transform.position.y < transform.position.y - .75f) && (GameObject.Find("Player").transform.position.y >= transform.position.y - 1.2f) 
		   && gameObject.GetComponent<SpriteRenderer>().sprite != null){
			GameControl.control.nearChest = true;
		}
		else{
			GameControl.control.nearChest = false;
		}

		if (chestOpened) {
			GameObject.Find("ChestContents").transform.position = new Vector3 (GameObject.Find("ChestContents").transform.position.x, GameObject.Find("ChestContents").transform.position.y + .01f, GameObject.Find("ChestContents").transform.position.z);
		}
	}

	void FixedUpdate(){
		spriteLinger --;
		if(Input.GetKeyDown("z") && GameControl.control.nearChest && !chestOpened && !GameControl.control.sceneTransitioning){
			spriteLinger = 59;
			chestOpened = true;
			GameObject newSprite = new GameObject("ChestContents");
			newSprite.AddComponent<SpriteRenderer>();
			newSprite.transform.position = GameObject.Find(currentChest).transform.position;
			newSprite.GetComponent<SpriteRenderer>().sortingOrder = 6;
			newSprite.transform.localScale = new Vector3(2, 2, 2);

			if(chestContents == "HeartContainer"){
				GameControl.control.pauseForScene();
				GameControl.control.AddHearts(1);

				/*GameControl.control.heartNum += 1;
				GameControl.control.health = GameControl.control.heartNum;

				string name = "Heart"; 
				name += GameControl.control.heartNum.ToString();
				GameObject newHeartSprite = new GameObject(name);
				
				string prevHeart = "Heart";
				int temp = GameControl.control.heartNum - 1;
				prevHeart += temp.ToString();
				
				newHeartSprite.AddComponent<SpriteRenderer>();
				newHeartSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/120px-Heart.svg");
				newHeartSprite.transform.position = new Vector3(GameObject.Find(prevHeart).transform.position.x + .68f, GameObject.Find(prevHeart).transform.position.y, GameObject.Find(prevHeart).transform.position.z);
				newHeartSprite.transform.parent = GameObject.Find("Main Camera").transform;
				newHeartSprite.transform.localScale = new Vector3(.5f, .5f, .5f);
				newHeartSprite.GetComponent<SpriteRenderer>().sortingOrder = 6;*/

				tempPlayer = GameObject.Find("Player").transform.position;
				GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/LinkGreenLttp");
				GameObject.Find("Player").GetComponent<BoxCollider2D>().enabled = false;
				GameObject.Find("Player").transform.position = tempPlayer;
				GameObject.Find("Player").transform.localScale = new Vector3(4, 4, 4);

				newSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/HeartContainerPickupLttP");

				//GameControl.control.UpdateHealth();
				GameControl.control.cameraTarget = "newSprite";
				GameControl.control.setPauseTimer(60);
			}
		}

		if (spriteLinger < 0 && chestOpened) {
			GameControl.control.unpauseGame();

			GameObject.Find("Player").transform.localScale = new Vector3(.9f, .9f, 1f);
			GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("zildaSpriteSheet_105");
			GameObject.Find("Player").transform.position = tempPlayer;
			GameObject.Find("Player").GetComponent<BoxCollider2D>().enabled = true;

			chestOpened = false;
			GameControl.control.nearChest = false;
			DestroyObject(GameObject.Find("ChestContents"));
			DestroyObject(gameObject);
		}
	}
}
