using UnityEngine;
using System.Collections;

public class DirectedCollision : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.name == "")
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;
            bool right = contactPoint.x > center.x;
            bool top = contactPoint.y > center.y;
        }
    }
}
