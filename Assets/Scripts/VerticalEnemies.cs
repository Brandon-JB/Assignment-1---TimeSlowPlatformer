using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class VerticalEnemies : MonoBehaviour

{ 
private float dirY;
public float moveSpeed;
private Rigidbody2D rb;
private bool facingRight = false;
private Vector3 localScale;
public GameObject Enemy;
public bool movingDown = true;

void Start()
{
    localScale = transform.localScale;
    rb = GetComponent<Rigidbody2D>();
    if (movingDown == true)
    {
        dirY = -1f;
    }

    else
    {
        dirY = 1f;
    }
}

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.tag == "Wall")
    {
        dirY *= -1f;
    }

    if (collision.gameObject.tag == "KillAura")
    {
        Destroy(gameObject);
    }
}

void FixedUpdate()
{
    rb.velocity = new Vector2(rb.velocity.x, dirY * moveSpeed);
}

private void LateUpdate()
{
    CheckWhereToFace();
}

void CheckWhereToFace()
{
    if (dirY > 0)
        facingRight = true;
    else if (dirY < 0)
        facingRight = false;

    if (((facingRight) && (localScale.y < 0)) || ((!facingRight) && (localScale.y > 0)))
        localScale.y *= -1f;

    transform.localScale = localScale;
}
}
