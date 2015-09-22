using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	
	public float health;			//Player Health
	public float maxHealth;
	private float healthPerHeart = 2f;
	
	public SpriteRenderer heartSprite;
	
	private ArrayList hearts = new ArrayList();
	
	
	void Awake() {
		if (GameControl.control != null) {
			GameControl.control.setHS (this);
		}
	}
	
	public void Start() {
		health = GameControl.control.health;
		if (!GameControl.control.healthInit) {
			GameControl.control.setHS (this);
			GameControl.control.healthInit = true;
			AddHearts (3);
		} else {
			GameControl.control.setHS(this);
			DrawHearts (GameControl.control.heartNum);
		}
		Debug.Log (health);
	}
	
	public void AddHearts(int n) {
		for (int i = 0; i < n; i++) {
			Transform newHeart = ((GameObject) Instantiate (heartSprite.gameObject, transform.position, Quaternion.identity)).transform;
			newHeart.parent = this.transform;
			
			
			for(int j = 0; j < GameControl.control.heartNum; j++) {
				newHeart.Translate(Vector3.right);
			}

			hearts.Add(newHeart);
			GameControl.control.heartNum++;
		}
		
		maxHealth += n * healthPerHeart;
		GameControl.control.maxHealth = maxHealth;
		health = maxHealth;
		GameControl.control.health = health;
		UpdateHealth ();
	}
	
	public void DrawHearts(int n) {
		for (int i = 0; i < n; i++) {
			Transform newHeart = ((GameObject) Instantiate (heartSprite.gameObject, transform.position, Quaternion.identity)).transform;
			newHeart.parent = this.transform;
			
			for(int j = 0; j < i; j++) {
				newHeart.Translate(Vector3.right);
			}

			hearts.Add(newHeart);
		}
		
		maxHealth = GameControl.control.maxHealth;
		UpdateHealth ();
	}
	
	public void ModifyHealth (int amount) {
		health += amount;
		health = Mathf.Clamp (health, 0, maxHealth);
		GameControl.control.health = health;
		UpdateHealth ();
	}
	
	public void UpdateHealth() {
		bool restAreEmpty = false;
		int i = 1;
		
		foreach (Transform heart in hearts) {
			if (restAreEmpty) {
				heart.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/120px-Empty_Heart.svg");
			}
			else {
				if(health >= i * healthPerHeart) {
					heart.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/120px-Heart.svg");
					if(health == i * healthPerHeart) {
						restAreEmpty = true;
					}
				}
				else {
					heart.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Half_Heart");
					restAreEmpty = true;
				}
			}
			i++;
		}
	}
}

