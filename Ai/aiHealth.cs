using UnityEngine;
using System.Collections;

public class aiHealth: MonoBehaviour {
	public float hp=2;
	private float dropChance=.5f;
	public aiHealth aiH;
	public bool heartContainer;
	public GameObject healingHeart;
	// Use this for initialization

	void Awake(){
		if (aiH == null) {
			aiH = this;
		}

	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	if (hp <= 0) {
			dropLoot();
			//destroy the ai
			healingHeart.transform.position=this.gameObject.transform.position;
				GameObject.Destroy (this.gameObject);

						
				}
		}
	public void takeDamage(float hit){
		hp -= hit;

	}
	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.CompareTag ("Weapon")) {
			//here we can add each individual weapon
			//easily giving them their own damage
			//sword not currently supported as its set up a little differently not sure
			if(other.collider.gameObject.name=="Boomerang"){
				hp-=.5f;
			}

			//take damage based on the weapon
			//sword =1 boomerang=1/2

				}

		}

	public void dropLoot(){
		//ai is killed roll to see if it drops a heart
		if (dropChance >= Random.Range (0f, 1f)) {
			healingHeart = (GameObject)Instantiate(Resources.Load("healingHeart"));
			healingHeart.transform.position=this.gameObject.transform.position;

			
		}
		if (heartContainer)
			GameControl.control.AddHearts (1);

	}

}
