using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float speed = 10f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;
    public bool PlayerCode = true;
    public GameObject Player;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] GameObject GameOverScreen;
    public AudioSource GameOver;
    public GameObject AudioObject;
    public float timeSlowMultiplyer;

    public class TimeScale
    {
        public static float player = 1;
        public static float enemies = 1;
        public static float global = 1;
    }

    void Start()
    {

    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.T))
        {
            TimeScale.global = 1 / timeSlowMultiplyer;
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            TimeScale.global = 1;
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
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Spikes")
        {
            GameOver.Play();
            GameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
