using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private bool Left, Right, Jump;
    private bool onGround;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Left)
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            if(rb.velocity.x < -maxSpeed)
            {
                rb.velocity = new Vector2(-maxSpeed, rb.position.y);
            }
            anim.SetFloat("Speed", 1f);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        } else if (Right)
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(maxSpeed, rb.position.y);
            }
            anim.SetFloat("Speed", 1f);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        } else if (Jump && onGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
            onGround = false;
        } else if(Left && Jump && onGround)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            rb.AddForce(new Vector2(-1f, -1f) * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
            onGround = false;
        }
        else if (Right && Jump && onGround)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            rb.AddForce(new Vector2(1f, 1f) * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
            onGround = false;
        }
    }

    public void LeftDown()
    {
        Left = true;
    }

    public void LeftUp()
    {
        Left = false;
    }

    public void RightDown()
    {
        Right = true;
    }

    public void RighUp()
    {
        Right = false;
    }

    public void JumpDown()
    {
        rb.gravityScale = 0f;
        Jump = true;
        StartCoroutine(delayJumpDown());
    }

    IEnumerator delayJumpDown()
    {
        yield return new WaitForSeconds(0.4f);
        rb.gravityScale = 5f;
        Jump = false;
    }

    public void JumpUp()
    {
        Jump = false;
    }

    public void Pause()
    {
        GameManager.Instance.status = GameManager.stateGame.PAUSE;
    }

    public void Continue()
    {
        GameManager.Instance.status = GameManager.stateGame.PLAY;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.status = GameManager.stateGame.WIN;
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("Died", true);
            GameManager.Instance.status = GameManager.stateGame.DIE;
        } else if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}
