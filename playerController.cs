
using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public Vector2 speed = new Vector2(1, 1);
	public int keys=0;
	private int direction;
	
	private Inventory inventory;
	private Animator animator;
	private float health = 0;
	private int heartNum;
	private bool knockedBack = false;
	private int knockBackTimer = 0;
	private Vector3 knockback;
	private int invulnTime = 0;
	private Vector2 pCoord;
	private Transform thingToPull = null;
	private bool pushpull = false;
	private Vector2 tempSpeed;
	private Vector2 contactPoint;
	FMOD_StudioEventEmitter emitter;
	
	public bool onIce = false;

	void Awake (){
		/*if (GameControl.control.health == 0) {
			GameControl.control.health = 3f;
			GameControl.control.maxHealth=GameControl.control.health;
		}*/
	}

	// Use this for initialization
	void Start (){
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
		animator = this.GetComponent<Animator>();
		health = GameControl.control.health;
		tempSpeed = speed + new Vector2 (0, 0);
		//hearts.Add(GameObject.FindGameObjectsWithTag("Health"));
		//GameControl.control.UpdateHealth ();
		emitter = GetComponent<FMOD_StudioEventEmitter> ();
		direction = GameControl.control.direction;
		animator.SetInteger ("direction", direction);
	}
	// Update is called once per frame
	void Update (){

		GameControl.control.direction = direction;
		knockBackTimer --;
		invulnTime --;
		if (health <= 0) {
			Application.LoadLevel("gameOverScene");
		}
		if (knockBackTimer <= 0) {
			knockedBack = false;
		}
		if(Input.GetKeyDown("z") && !knockedBack && !GameControl.control.sceneTransitioning && inventory.equippedItem != null) {
			
			//Sword is equipped
			if(inventory.equippedItem.itemID == 0) {
				int d = animator.GetInteger("direction");
				if(d == 0 || d == 1 || d == 7) {
					animator.SetTrigger ("AttackUp");
				} 
				else if (d == 2){
					animator.SetTrigger ("AttackRight");
				}
				else if(d == 4 || d == 3 || d == 5) {
					animator.SetTrigger ("AttackDown");
				}
				else if(d == 6) {
					animator.SetTrigger ("AttackLeft");
				}
				//FMOD_StudioSystem.instance.PlayOneShot("/Weapons/Sword", this.transform.position);
				//emitter.Play();
			}
			
			else if(inventory.equippedItem.itemType == Item.ItemType.Destroyer) {
				destroyBlocks(inventory.equippedItem.itemID);
			}
		}		
				bool pushpull = Input.GetKey (KeyCode.LeftShift);
				Vector2 pCoord = transform.position;
				if (thingToPull != null) {
						if (!pushpull) {
								thingToPull.rigidbody2D.velocity = new Vector2 (0, 0);
								speed = new Vector2(5,5);
						}
						Vector2 d = transform.position - thingToPull.position;

						float xmin = thingToPull.collider2D.bounds.min.x;
						float xmax = thingToPull.collider2D.bounds.max.x;
						float ymin = thingToPull.collider2D.bounds.min.y;
						float ymax = thingToPull.collider2D.bounds.max.y;
						Vector2 pullDir;
						float distance;
						float pullF;
						float pDir = 1;
						pCoord = transform.position;
						//FMOD_StudioSystem.instance.PlayOneShot("/Weapons/Sword", this.transform.position);
						pullDir = new Vector2 (0, 0);
						pullF = 15;
						if ((pCoord.y > ymin && pCoord.y < ymax) && pCoord.x > xmax) {//player on right
								//d = new Vector2 (transform.rigidbody2D.velocity.x, 0);
								d.y = 0;
								if (Input.GetAxis ("Horizontal") < 0) {
										//thingToPull.rigidbody2D.isKinematic=false;
										pullDir = -d;
										pDir = -1;
								} else if (Input.GetAxis ("Horizontal") > 0) {
										pullDir = d;
										pDir = -1;
										//thingToPull.rigidbody2D.isKinematic=true;
								}
						} else if ((pCoord.x > xmin && pCoord.x < xmax) && pCoord.y > ymax) {//player above
								d.x = 0;
								if (Input.GetAxis ("Vertical") < 0) {
										pullDir = -d;
										pDir = -1;
								} else if (Input.GetAxis ("Vertical") > 0) {
										pullDir = d;
								}
						} else if ((pCoord.x > xmin && pCoord.x < xmax) && pCoord.y < ymin) {//player below
								d.x = 0;
								if (Input.GetAxis ("Vertical") > 0) {
										pullDir = -d;
								} else if (Input.GetAxis ("Vertical") < 0) {
										pullDir = d;
								}
						} else if ((pCoord.y > ymin && pCoord.y < ymax) && pCoord.x < xmin) {//player on left 
								d.y = 0;
								if (Input.GetAxis ("Horizontal") > 0) {
										pullDir = -d;
								} else if (Input.GetAxis ("Horizontal") < 0) {
										pullDir = d;
								}

						}
						distance = d.magnitude;
						//print (distance);
						if (distance > 1) {
								thingToPull = null;
						} else if (distance > 0 && pushpull) {
								thingToPull.rigidbody2D.velocity = pullDir * (pullF * Time.deltaTime);
								speed = thingToPull.rigidbody2D.velocity;
								if (pDir == -1)
										speed = -speed;
						}
						if (Input.GetAxis ("Horizontal") == 0 && Input.GetAxis ("Vertical") == 0)
								thingToPull.rigidbody2D.velocity = new Vector2 (0, 0);
				} else {
						GameObject[] pp = GameObject.FindGameObjectsWithTag ("pushpull");
						foreach (GameObject obj in pp)
								obj.transform.rigidbody2D.velocity = new Vector2 (0, 0);
				}
	}

	void FixedUpdate(){
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		ManageMovement(h, v);
		if (knockedBack) {
			knockback.Normalize();
			rigidbody2D.velocity = 4 * knockback;
		}
		
	}
	void ManageMovement(float horizontal,float vertical){
		if ((horizontal != 0f || vertical != 0f) && !knockedBack && !GameControl.control.sceneTransitioning) {
			animator.SetBool ("moving", true);
			animateWalk (horizontal, vertical);
		} else {
			animator.SetBool ("moving", false);
		}
		if (!knockedBack && !GameControl.control.sceneTransitioning) {
			Vector3 movement = new Vector3 (horizontal * speed.x, vertical * speed.y, 0);
			if(!onIce) {
				rigidbody2D.velocity = movement;
			} else {
				rigidbody2D.AddForce(movement);
			}
		}
	}
	void animateWalk(float h,float v){
		if(animator){
			if ((v > 0)&&(v>h)){	//Up
				direction = 0;
				//animator.SetInteger ("direction", direction);
			}
			if ((h > 0)&&(v<h)){	//Right
				direction = 2;
				//animator.SetInteger ("direction", direction);
			}
			if ((v < 0)&&(v<h)){	//Down
				direction = 4;
				//animator.SetInteger ("direction", direction);
			}
			if ((h < 0 )&&(v>h)){	//Left
				direction = 6;
				//animator.SetInteger ("direction", direction);
			}
			if((v > 0) && (h > 0)) {	//Up and Right
				direction = 1;
				//animator.SetInteger("direction", direction);
			}
			if((v < 0) && (h > 0 )) {	//Down and Right
				direction = 3;
			}
			if((v < 0) && (h < 0)) {	//Down and Left
				direction = 5;
			}
			if((v > 0) && (h < 0)) {	//Up and Left
				direction = 7;
			}
			animator.SetInteger("direction", direction);
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.CompareTag("Enemy") && invulnTime <= 0) {
			if(col.gameObject.ToString().Contains("Zol")){
				//GameControl.control.health -= .5f;
				GameControl.control.ModifyHealth(-1);
				health = GameControl.control.health;
				knockedBack = true;
				knockBackTimer = 30;
				invulnTime = 75;
				knockback = GameObject.Find("Player").transform.position - col.gameObject.transform.position;
				//GameControl.control.UpdateHealth ();
			}
		} else if (col.gameObject.CompareTag ("pushpull")) {
				thingToPull = col.gameObject.transform;
		}
		if(col.gameObject.CompareTag("HP Pickup")){
			if(col.gameObject.ToString().Contains("Fairy")){
				//GameControl.control.health = 4f;
				//GameControl.control.UpdateHealth();
				GameControl.control.ModifyHealth(6);
				DestroyObject(col.gameObject);
			}
		}
		//if the player picks up a floating heart
		if (col.gameObject.CompareTag ("Health")) {
			//destroy the heart
			Destroy(col.collider.gameObject);
			//if room to heal
			if(GameControl.control.health<GameControl.control.maxHealth){
				//GameControl.control.health += .5f;
				GameControl.control.ModifyHealth(2);
				health = GameControl.control.health;
				//GameControl.control.UpdateHealth();
			}
		}
		if (col.gameObject.ToString ().Contains("Projectile")) {
			GameControl.control.ModifyHealth (-1);
			health=GameControl.control.health;
		}
	}
	void OnCollisionExit2D (Collision2D col)
	{
		if (col.gameObject.CompareTag ("pushpull")) {
			if (col.gameObject.Equals (GameObject.Find ("box"))) {
				col.gameObject.transform.rigidbody2D.velocity = new Vector2 (0, 0);
				speed = new Vector2(5,5);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("HP Pickup")){
			if(other.gameObject.ToString().Contains("Fairy")){
				//GameControl.control.health = 4f;
				//GameControl.control.maxHealth=GameControl.control.health;
				//GameControl.control.UpdateHealth();
				GameControl.control.ModifyHealth(6);
				DestroyObject(other.gameObject);
			}
		}
		if(other.gameObject.CompareTag("Ice")) {
			onIce = true;
			Debug.Log("Cool");
		}
			

	}
	
	public void OnIce() {
		onIce = true;
		Debug.Log("Cool");
	}
	
	public void OffIce() {
		onIce = false;
		Debug.Log("Hot");
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.CompareTag("Ice"))
			onIce = false;
	}

	void destroyBlocks(int itemID) {
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Destroyable")) {
            if(isInProximity(block))
				block.GetComponent<destroyable>().destroy(itemID);
        }
	}
	
	bool isInProximity(GameObject block) {
		Vector3 playerPosition = gameObject.GetComponent<Transform>().position;
		Vector3 blockPosition = block.GetComponent<Transform>().position;
		bool close = Mathf.Abs(playerPosition.x - blockPosition.x) < 1.5 && Mathf.Abs(playerPosition.y - blockPosition.y) < 1.5;
		bool facing = false;
		if(direction == 1) {
			facing = playerPosition.y < blockPosition.y;
		} else if(direction == 2) {
			facing = playerPosition.x < blockPosition.x;
		} else if(direction == 3) {
			facing = playerPosition.y > blockPosition.y;
		} else if(direction == 4) {
			facing = playerPosition.x > blockPosition.x;
		}
		return close && facing;
	}
}