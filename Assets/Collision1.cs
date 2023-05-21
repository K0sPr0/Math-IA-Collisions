using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collision1 : MonoBehaviour
{
    [HideInInspector]
    public float size;
    [HideInInspector]
    float wallSize;
    public Rigidbody2D rb;
    private SpriteRenderer renderer;
    private SpriteRenderer wallRenderer;
    public GameObject wall;
    public GameObject obj2;
    public Collision2 other;
    public TMP_Text bounceText;
    public TMP_Text object1Text;
    public TMP_Text object2Text;
    public TMP_Text noMoreBounces;
    public float maxVel;
    public float velocity;
    public float slowMoVelocity;
    // [HideInInspector]
    public float inVelocity;
    public float mass;
    public float seconds;
    public float timeSinceLastBounce;
    public float bounces;
    bool collision;
    bool willBounce;

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("BouncesPerSecond", .01f, 1.0f);

        renderer = GetComponent<SpriteRenderer>();
        size = renderer.bounds.size.x;

        wallRenderer = wall.GetComponent<SpriteRenderer>();
        wallSize = wallRenderer.bounds.size.x;

        velocity = 0;
        timeSinceLastBounce = 1;
        bounces = 0;
        collision = false;
        willBounce = true;

        rb.freezeRotation = true;

        other.xFreezePos=(wall.transform.position.x + wallSize/2)+size+other.size/1.75f;
    }
     
    private void OnTriggerEnter2D(Collider2D oth) 
    {
        collision=true;
        if (oth.gameObject.tag != "Wall"){
            velocity = other.inVelocity * ((2 * other.mass) / (other.mass + mass)) + inVelocity * ((mass - other.mass) / (other.mass + mass));
            other.velocity = other.inVelocity * ((other.mass - mass) / (other.mass + mass)) + inVelocity *((2 * mass)/(other.mass + mass));
        }
        else 
        {
            velocity = -velocity;
        }
        Debug.Log("collide");

        timeSinceLastBounce=seconds;
        seconds=0;
        bounces+=1;
        collision=false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        seconds += Time.deltaTime*(timeSinceLastBounce*10);
        slowMoVelocity = velocity;
        
        bounceText.text = "Bounces:"+ bounces.ToString();

        if (velocity<0)
        {
            object2Text.text = "m2:" + mass.ToString() + "\nv2:" + velocity.ToString();
        }
        else
        {
            object2Text.text = "m2:" + mass.ToString() + "\nv2: " + velocity.ToString();
        }
        
        if (other.velocity<0)
        {
            object1Text.text = "m1:100^" + other.n.ToString() + "\nv1:" + other.velocity.ToString();
        }
        else
        {
            object1Text.text = "m1:100^" + other.n.ToString() + "\nv1: " + other.velocity.ToString();
        }

        if (slowMoVelocity>maxVel)
        {
            slowMoVelocity=maxVel;
        }
        else if (slowMoVelocity<-maxVel)
        {
            slowMoVelocity=-maxVel;
        }

        if (other.velocity > Mathf.Abs(velocity) && velocity>=0)
        {
            willBounce = false;
        }
        noMoreBounces.enabled = !willBounce;

        if (!collision)
        {
            inVelocity = velocity;
            other.inVelocity = other.velocity;
        }
        rb.velocity = new Vector2(slowMoVelocity, 0);
    }
}
