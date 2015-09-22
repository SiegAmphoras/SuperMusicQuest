using UnityEngine;
using System.Collections;

public class LineMovement : MonoBehaviour {
	//used for straight moving proectiles
	// Use this for initialization
	private Vector3 playerPosition;
	private int projectileSpeed=3;
	void Start () {
		playerPosition=GameObject.Find("Player").transform.position - transform.position ;
		Destroy (gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = playerPosition.normalized*projectileSpeed;
	}
}
