using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPlayerToSpawn : MonoBehaviour
{
    GameObject Spawn;
    private void Start()
    {
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Transform>().position = Spawn.transform.position;
        }
    }
}
