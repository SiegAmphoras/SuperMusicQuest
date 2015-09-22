using UnityEngine;
using System.Collections;
public class AreaMovement : MonoBehaviour
{
    //new direction
    public float timer = 5.0f;
    public int stunTimer = 0;
    bool left = false;
    bool right = true;
    //how often can cause damage
    public float damageFrequency = 1.0f;
    //how often can be damaged
    private Vector2 nTransform = new Vector2(1, 1);
    //private float speed=1f;
    public AreaMovement random;
    public bool stunned = false;
    public float xPos;
    public float yPos;
    // Use this for initialization
    void Start()
    {
    xPos= transform.position.x;
    yPos = transform.position.y;
        if (random == null)
        {
            random = this;
        }

        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned && !GameControl.control.sceneTransitioning)
        {
            stunTimer--;
            gameObject.GetComponent<Animator>().enabled = false;
            rigidbody2D.velocity = new Vector3(0, 0, 0);
        }
        if (stunTimer <= 0 && stunned)
        {
            gameObject.GetComponent<Animator>().enabled = true;
            stunned = false;
            left = false;
            right = true;
        }
        //set the bounds of the area, 4
        //TODO: make its own script, not hardcodedm return to start, or attack
        if (transform.position.x - xPos > 4 || transform.position.x - xPos < -4)
        {
            ChangeDirection();
            Debug.Log("box");
        }
        if (transform.position.y - yPos > 4 || transform.position.x - yPos < -4)
        {
            ChangeDirection();
            Debug.Log("box2");
        }
    }

    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0 && !GameControl.control.sceneTransitioning && !stunned)
        {
            ChangeDirection();
        }
        if (!GameControl.control.sceneTransitioning && !stunned)
        {
            rigidbody2D.velocity = nTransform;
        }
        if (stunTimer <= 30 && stunned)
        {
            if (left)
            {
                left = false;
                right = true;
                transform.position = new Vector3(transform.position.x - .05f, transform.position.y, 0);
            }
            else if (right)
            {
                right = false;
                left = true;
                transform.position = new Vector3(transform.position.x + .05f, transform.position.y, 0);
            }
        }
    }
    //chages ai's direction every timer seconds
    void ChangeDirection()
    {

        nTransform = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        nTransform.Normalize();
        if (nTransform.x == 0 && nTransform.y == 0)
            ChangeDirection();
        timer = 5f;
    }

    //ran into player
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.Equals(GameObject.Find("Player")) && !GameControl.control.sceneTransitioning && !stunned)
        {
            ChangeDirection();
        }

    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.Equals(GameObject.Find("Player")) && !GameControl.control.sceneTransitioning && !stunned)
        {
            ChangeDirection();
        }
    }
}