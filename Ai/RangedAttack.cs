using UnityEngine;
using System.Collections;

public class RangedAttack : MonoBehaviour
{
	private Vector3 moveRay = new Vector3 (0, 0, 0);
	private Vector3 moveAmount = new Vector3 (.3f, -.2f, 0);
	private float attackTime = 3f;
	private float destroyTimer = 2.5f;
	private GameObject projectile;
	private float projSpeed = 5;
	private RangedAttack attack;
	private float toMove;
	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		toMove = projSpeed * Time.deltaTime;
		destroyTimer -= Time.deltaTime;
		attackTime -= Time.deltaTime;
		moveRay = transform.position;
		//prevents raycast from running into own collider
		moveRay += moveAmount;
		//draw a line in front of the ai
		RaycastHit2D hit = Physics2D.Raycast (moveRay, (-transform.up) * 4, 4);

		if (hit != null && hit.collider != null) {

			//the enemy can see the player
			if (hit.collider == GameObject.Find ("Player").collider2D) {
				if (attackTime <= 0) {
					attackTime = 3;
					Fire ();
					//launch a projectile
				}
			}

		}
		if (destroyTimer <= 0) {
			destroyTimer = 2.5f;
			//need to destroy projectile, need another script?

		}
	
	}

	public void Fire ()
	{

		//commented out for submission(doesnt move yet just sits there)
		projectile = (GameObject)Instantiate (Resources.Load ("OctoProjectile"));
		projectile.transform.position = this.gameObject.transform.position;
		projectile.transform.rotation = this.gameObject.transform.rotation;
		Physics2D.IgnoreCollision (projectile.collider2D, collider2D);
		
		
		//need to translate the projectile, not the enemy firing it
		//transform.Translate (-Vector3.back * toMove);
		
	}

	public void Fire (float speed)
	{
		projSpeed = speed;
		Fire ();
	}
}


