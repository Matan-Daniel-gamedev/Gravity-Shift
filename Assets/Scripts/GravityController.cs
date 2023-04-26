using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public bool toggle = false;
    public float multiplier = 5;
    public float cooldownAmount = 0.1f;
    Vector2 vec = Vector2.zero;

    float cooldown=0;
    float forceMultiplier = 10f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (cooldown <= 0)
            {
                toggle = !toggle;
                vec *= -1;
                cooldown = cooldownAmount;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }

        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(vec, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool hitWall = collision.gameObject.name.Contains("Walls");
        if (hitWall)
        {
            Vector2 surfaceNormal = collision.contacts[0].normal;
            vec = -surfaceNormal * forceMultiplier;
        }
    }
}
