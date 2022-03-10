using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingRight = true;
    public bool isMoving = false;
    public bool Jump = false;

    public float JumpForce = 500f;
    public float GroundCheckDistance;
    public float Speed = 5f;
    public float CliffJumpTime = .3f;
    public float telePortTimerCount = 17;

    private float cliffJumpTimer;
    private float h;
    private float TeleportTimer;

    private bool grounded = false;
    private bool killable = true;
    private bool timerOk = false;

    private Vector3 startPosition;

    private Animator anim;

    private Rigidbody2D RB2D;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Need an animator over here!");
        }

        RB2D = GetComponent<Rigidbody2D>();
        if (RB2D == null)
        {
            Debug.Log("Where is your rigidbody2d?!");
        }

    }

    public void onReset()
    {
        killable = true;

        Debug.Log("Reset Player");

        GameStateManager.Instance.onDeath();

        transform.SetPositionAndRotation(startPosition, Quaternion.identity);

        Camera.main.transform.SetPositionAndRotation(startPosition + Vector3.forward * - 10, Quaternion.identity);
        RB2D.velocity = Vector3.zero;
    }

    private void onTeleportTimer()
    {
        if (TeleportTimer > telePortTimerCount)
        {
            onTeleport();
        }
    }

    private void onTeleport()
    {
        transform.SetPositionAndRotation(startPosition, Quaternion.identity);

        Camera.main.transform.SetPositionAndRotation(startPosition + Vector3.forward * -10, Quaternion.identity);
        RB2D.velocity = Vector3.zero;
        
        timerOk = false;
        TeleportTimer = 0;
    }

    private void Update()
    {
        onTeleportTimer();

        anim.SetBool("Grounded", grounded);

        grounded = Physics2D.Linecast(transform.position, transform.position + Vector3.up * GroundCheckDistance,
            1 << LayerMask.NameToLayer("Ground"));

        if (timerOk == true)
        {
            TeleportTimer += Time.deltaTime;
        }

        if (grounded)
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.up * GroundCheckDistance, Color.green);
        }

        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.up * GroundCheckDistance, Color.red);
        }

        if (Input.GetButtonDown("Jump") && cliffJumpTimer > 0)
        {
            Jump = true;
        }

        if (Input.GetButtonUp("Jump") && RB2D.velocity.y > 0)
        {
            RB2D.velocity = new Vector2(RB2D.velocity.x, RB2D.velocity.y * .5f);
        }

        if (grounded)
        {
            cliffJumpTimer = CliffJumpTime;
        }

        else
        {
            cliffJumpTimer -= Time.deltaTime;
        }        
    }

    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * h * Speed * Time.deltaTime);

        if (h != 0)
        {
            isMoving = true;
        }

        if (h > 0 && !FacingRight)
        {
            Flip();
        }

        else if (h < 0 && FacingRight)
        {
            Flip();
        }

        anim.SetBool("isMoving", isMoving);

        if (Jump)
        {
            RB2D.AddForce(new Vector2(0, JumpForce));

            Jump = false;
        }
    }

    void Flip()
    {
        FacingRight = !FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileController projectile = collision.gameObject.GetComponent<ProjectileController>();

        if (projectile != null && killable == true)
        {
            killable = false;
            onReset();
        }

        if (collision.gameObject.CompareTag("Hazard") && killable == true)
        {
            killable = false;
            onReset();
        }

        if (collision.gameObject.CompareTag("Invis"))
        {
            timerOk = true;
        }
    }
}