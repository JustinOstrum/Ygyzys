using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMovement : MonoBehaviour
{
    public bool Jump = false;

    public float JumpForce = 450f;

    public float GroundCheckDistance;

    private bool grounded = false;

    private Animator anim;

    private Rigidbody2D RB2D;


    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Need an animator over here!");
        }

        RB2D = GetComponent<Rigidbody2D>();
        if (RB2D == null)
        {
            Debug.LogError("Where is your rigidbody2d?!");
        }

    }

    
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, transform.position + Vector3.up * GroundCheckDistance, 
            1 << LayerMask.NameToLayer("Ground"));

        if (grounded)
            Debug.DrawLine(transform.position, transform.position + Vector3.up * GroundCheckDistance, Color.green);
        else
            Debug.DrawLine(transform.position, transform.position + Vector3.up * GroundCheckDistance, Color.red);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump = true;
        }

        if(Input.GetButtonUp("Jump") && RB2D.velocity.y > 0)
        {
            RB2D.velocity = new Vector2(RB2D.velocity.x, RB2D.velocity.y * .5f);
        }

        anim.SetBool("Grounded", grounded);
    }

    private void FixedUpdate()
    {
        if (Jump)
        {
            RB2D.AddForce(new Vector2(0, JumpForce));

            Jump = false;
        }
    }
}
