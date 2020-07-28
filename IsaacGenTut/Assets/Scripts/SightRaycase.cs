using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightRaycase : MonoBehaviour
{
    // Float a rigidbody object a set distance above a surface.

    public float floatHeight;     // Desired floating height.
    //public float liftForce;       // Force to apply when lifting the rigidbody.
    //public float damping;         // Force reduction proportional to speed (reduces bouncing).
    //public GameObject Player;
    Rigidbody2D rb2D;
    Collider circleCol;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCol = GetComponent<Collider>();
    }

    void FixedUpdate()
    {

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        Debug.DrawRay(transform.position, Vector2.up, Color.green);
        transform.Rotate(0, 0, 1f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

        // If it hits something...
        if (hit.collider.gameObject.tag == ("Player"))
        {
            this.gameObject.transform.Rotate(0, 0, 10f);
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("I hit player");
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            float heightError = floatHeight - distance;

            // The force is proportional to the height error, but we remove a part of it
            // according to the object's speed.
            //float force = liftForce * heightError - rb2D.velocity.y * damping;

            // Apply the force to the rigidbody.
            //rb2D.AddForce(Vector3.up * force);
        }
    }
}
