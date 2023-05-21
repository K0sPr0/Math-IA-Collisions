using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision2 : MonoBehaviour
{
    [HideInInspector]
    public float size;
    private SpriteRenderer renderer;
    public Rigidbody2D rb;
    public float velocity;
    // [HideInInspector]
    public float inVelocity;
    public float n;
    public float mass;
    public float checkVel;
    // [HideInInspector]
    public float xFreezePos;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        size = renderer.bounds.size.x;

        velocity = -1f;

        rb.freezeRotation = true;
        mass = Mathf.Pow(100,(n));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(velocity, 0);

        checkVel =Mathf.Pow(0.9999998f,Mathf.Pow(10,7-n));

        if (gameObject.transform.position.x <=xFreezePos && velocity<checkVel) {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else 
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
