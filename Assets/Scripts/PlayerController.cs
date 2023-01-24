using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float speed = 10f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] GameObject GameOverScreen;
    public AudioSource GameOver;
    public GameObject AudioObject;

    public float characterSpeedUp = 5f;
    public float gravityChange;
    public float jumpChange;

    public SpriteRenderer sr;

    public GameObject killAura;

    public TimeManager timeManager;
    public GameObject timeObject;

    private float hitCount = 2f;

    void Start()
    {
        killAura.SetActive(false);

        timeObject.SetActive(true);

        hitCount = 2f;

        sr.color = Color.white;
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            timeManager.makeSlow();
            speed = speed * characterSpeedUp;
            rb.gravityScale = rb.gravityScale * gravityChange;
            jumpingPower = jumpingPower * jumpChange;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            timeManager.makeFast();
            speed = speed / characterSpeedUp;
            rb.gravityScale = rb.gravityScale / gravityChange;
            jumpingPower = jumpingPower / jumpChange;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            killAura.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            killAura.SetActive(false);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown("escape"))
        {
            GameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }

        flip();

        if (hitCount <= 0)
        {
            Death();
        }

        if (hitCount == 1)
        {
            sr.color = Color.red;
        }
    }

    private void fixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    }

    private void flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            //Vector3 localScale = transform.localScale;
            //localScale.x *= -1f;
            //transform.localScale = localScale;

            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            /*GameOver.Play();
            GameOverScreen.SetActive(true);*/
            hitCount = hitCount - 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Death();
        }
    }

    private void Death()
    {
            SceneManager.LoadScene("Game Over");
    }
    
}
