using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private float shieldHealth = 3f;
    public GameObject Shield;
    public SpriteRenderer sr;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        shieldHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldHealth == 3)
        {
            sr.color = new Color(0, 1, 0, (float).2);
        }

        if (shieldHealth == 2)
        {
            sr.color = new Color(0, 0, 1, (float).2);
        }

        if (shieldHealth == 1)
        {
            sr.color = new Color(1, 0, 0, (float).2);
        }

        if (shieldHealth <= 0)
        {
            Shield.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            shieldHealth = shieldHealth - 1;

            if (shieldHealth == 0)
            {
                audioSource.Play();
            }
        }
    }
}
