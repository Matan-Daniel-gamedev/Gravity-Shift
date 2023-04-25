using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public bool toggle = false;
    public float multiplier = 5;
    Vector2 vec = Vector2.zero;
    float cooldown=0;
    // Start is called before the first frame update
    void Start()
    {

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
                cooldown = 0.1f;
                //Physics2D.gravity *= -1;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }

        }
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(vec, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Walls"))
        {
            Vector2 surfaceNormal = collision.contacts[0].normal;
            float angle = Vector3.Angle(surfaceNormal, Vector3.up);
            vec = -surfaceNormal * 10f;
        }
    }
}
