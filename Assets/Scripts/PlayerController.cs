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

    public float characterSpeedUp = 5f;
    public float gravityChange;
    public float jumpChange;

    public SpriteRenderer sr;

    public GameObject killAura;

    public TimeManager timeManager;
    public GameObject timeObject;

    private float hitCount = 2f;

    public static bool game_paused = false;
    public GameObject pauseMenu;


    void Start()
    {
        killAura.SetActive(false);

        timeObject.SetActive(true);

        hitCount = 2f;

        sr.color = Color.white;
        game_paused = false;
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Mouse1) && game_paused == false)
        {
            timeManager.makeSlow();
            speed = speed * characterSpeedUp;
            rb.gravityScale = rb.gravityScale * gravityChange;
            jumpingPower = jumpingPower * jumpChange;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && game_paused == false)
        {
            timeManager.makeFast();
            speed = speed / characterSpeedUp;
            rb.gravityScale = rb.gravityScale / gravityChange;
            jumpingPower = jumpingPower / jumpChange;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && game_paused == false)
        {
            killAura.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && game_paused == false)
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

        flip();

        if (hitCount <= 0)
        {
            Death();
        }

        if (hitCount == 1)
        {
            sr.color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && game_paused == false)
        {
            game_paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && game_paused == true)
        {
            game_paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;

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

        if (collision.gameObject.tag == "WinGate")
        {
            SceneManager.LoadScene("Win Screen");
            Time.timeScale = 1f;
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
        Time.timeScale = 1f;
    }
    
}
