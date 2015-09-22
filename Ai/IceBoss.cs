using UnityEngine;
using System.Collections;
public class IceBoss : MonoBehaviour
{

	public enum BossState
	{
		Moving,
		Shooting,
		Stunned,
		Reset
	}
	private Texture shadow;
	private bool drawShadow=false;
	private BossState currentState;
	private float stateTime;
	private float mapX;
	private float mapY;
	private bool inAir;
	private Vector2 moveTo;
	private bool stomped;
	private Vector2 stompTarget;
	private float reload = .25f;
	private float speed = 7f;
	private RangedAttack rangedScript;
	// Use this for initialization
	void Start ()
	{
		//shadow = Resources.Load ("Sprites/Enemies/IceShadow")as Texture;
		currentState = BossState.Shooting;
		stateTime = 0;
		mapX = transform.position.x;
		mapY = transform.position.y;
		gameObject.AddComponent<RangedAttack>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//print (transform.position.x + "," + transform.position.y + "\n");
		//if(drawShadow)
		//Graphics.DrawTexture (new Rect (stompTarget.x, stompTarget.y, 10, 10), shadow);
		stateTime += Time.deltaTime;
		switch (currentState) {
		case BossState.Moving:
			if (!inAir&&!stomped) {
				collider2D.enabled=false;
				stompTarget=GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
				Jump (stompTarget.x);
			}
			else if (inAir&&!stomped) {
				if(!stomped && transform.position==(Vector3)moveTo){
				Stomp (stompTarget);
					break;
				}
				else if(!stomped){
					updatePosition(moveTo);
					break;}

			}
			else if(stomped){
				inAir=false;
				if(transform.position==(Vector3)stompTarget){
					collider2D.enabled=true;
					stomped=false;
					currentState=BossState.Shooting;
					drawShadow=false;
					stateTime=0;
				}
				else{
					updatePosition(stompTarget);
				}
				break;
			}

			break;
			
		case BossState.Shooting:
			inAir=false;
			collider2D.enabled=true;
			if(stateTime>5.5){
				stateTime=0;
				currentState=BossState.Moving;
				break;
			}

			if (stateTime < 5&&stateTime>1&&reload<=0) {
				reload=.25f;
				gameObject.GetComponent<RangedAttack>().Fire(20);
			}
			else
				reload-=Time.deltaTime;

			break;

		case BossState.Stunned:
			if (stateTime >= 5) {

				//Jump ();
			}

			break;
		case BossState.Reset:
			Stomp(new Vector2(mapX,mapY));
			break;
		}
		
	}

	private void Jump (float x)
	{
		moveTo = new Vector2 (x, transform.position.y + 5);
		inAir = true;
	}

	private void Stomp (Vector2 targetPos)
	{
		drawShadow = true;
		stomped = true;

	}
	
	private void updatePosition(Vector2 targetPos){
	if (Mathf.Abs (transform.position.x - targetPos.x) < 1f && Mathf.Abs (transform.position.y - targetPos.y) < 1f)
			transform.position = targetPos;
	if (transform.position != (Vector3)targetPos)
			transform.position += ((Vector3)targetPos - transform.position).normalized * speed * Time.deltaTime;
	}
}