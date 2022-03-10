using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;

    public bool DoGroundCheck;

    public bool grounded = true;

    public bool Moving;

    public bool wallTouchLeft = false;

    public bool wallTouchRight = false;

    public float GroundCheckDistance;

    public float LeftCheckDistance;

    public float RightCheckDistance;

    float currentSpeed;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = Speed;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.Linecast(
            transform.position,
            transform.position + Vector3.up * GroundCheckDistance,
            1 << LayerMask.NameToLayer("Ground"));

        if (!grounded)
        {
            currentSpeed = -1 * currentSpeed;
            transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sign(currentSpeed));

            Flip();
        }

        transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sign(currentSpeed) * Speed);

        wallTouchLeft = Physics2D.Linecast(transform.position, transform.position + Vector3.left * LeftCheckDistance,
            1 << LayerMask.NameToLayer("Ground"));

        wallTouchRight = Physics2D.Linecast(transform.position, transform.position + Vector3.right * RightCheckDistance,
            1 << LayerMask.NameToLayer("Ground"));

        if (Speed > 0)
        {
            Moving = true;
        }

        if (wallTouchLeft)
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.left * LeftCheckDistance, Color.green);
        }

        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.left * LeftCheckDistance, Color.red);
        }

        if (wallTouchRight)
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.right * RightCheckDistance, Color.green);
        }

        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.right * RightCheckDistance, Color.red);
        }

        if (wallTouchLeft)
        {
            currentSpeed = -1 * currentSpeed;
            transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sign(currentSpeed));
        }

        if (wallTouchRight)
        {
            currentSpeed = -1 * currentSpeed;
            transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sign(currentSpeed));
        }

        anim.SetBool("isMoving", Moving);

        anim.SetBool("Grounded", grounded);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
