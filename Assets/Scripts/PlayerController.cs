using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float Speed = 4f;
    Rigidbody2D rb;
    Transform tf;

    public float Gravity = -20f;

    public float JumpInitialVelocity = 30f;

    public Vector2 Direction {get; protected set;}
    public Vector2 Velocity {get; protected set;}
    bool jumpInputDown;
    bool jumpInputUp;

    private bool _collisionBottom;
    public bool CollisionBottom 
    {
        get { return _collisionBottom; }
        protected set {
            _collisionBottom = value;
            if(_collisionBottom)
            {
                ClearVelocityY();
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        Direction = Vector2.zero;
        Velocity = Vector2.zero;
        CollisionBottom = false;
    }

    public void SetDirectionalInput(Vector2 direction)
    {
        Direction = new Vector2(direction.x,0);
    }

    public void OnJumpInputDown()
    {
        jumpInputDown = true;
    }

    public void OnJumpInputUp()
    {
        jumpInputUp = true;
    }

    private void ClearState()
    {
        jumpInputDown = false;
        jumpInputUp = false;
    }

    void ClearVelocityY()
    {
        Velocity = new Vector2(Velocity.x,0f);
    }

    float hitDistance;

    void RayCastsVertical ()
    {
        Vector2 origin = new Vector2(tf.position.x,tf.position.y - 1f);
        Debug.DrawRay(origin,Vector3.down,Color.red);
        float rayDistance = Mathf.Max(Mathf.Abs(Velocity.y),Mathf.Abs(Gravity));
        RaycastHit2D hit = Physics2D.Raycast(origin,Vector2.down,rayDistance,LayerMask.GetMask("Block"));
        if(hit.collider)
        {
            hitDistance = hit.distance;
            //Debug.DrawRay(hit.point,hit.distance * Vector3.down,Color.magenta);
            Debug.DrawLine(origin,hit.point,Color.magenta);
            Debug.Log(hit.distance);
            CollisionBottom = hit.distance <= 0.02f;
            //Debug.Break();
        }
    }

    void Update()
    {
        RayCastsVertical();

        float velocityY = Velocity.y;
        // if(jumpInputDown)
        // {
            
        // }
        if(!CollisionBottom)
        {
            velocityY += Gravity * Time.deltaTime;
        }
        float velocityX = Direction.x * Speed;
        Velocity = new Vector2(velocityX,velocityY);
        rb.velocity = Velocity;

        ClearState();
    }

    ContactPoint2D contactPoint2D;

    private void OnCollisionStay2D(Collision2D other) 
    {
        ContactPoint2D contact = other.GetContact(0);
        contactPoint2D = contact;
        if(contact.collider.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            //CollisionBottom = true;
            //Velocity = new Vector2(Velocity.x,0f);
        }
    }
}
