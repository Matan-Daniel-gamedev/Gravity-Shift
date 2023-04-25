using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PullPlayer : MonoBehaviour
{
    GameObject player;
    public float force = 1.0f;
    public Vector2 direction = new Vector2(1, 0);
    Vector3 actualPos;

    public Tilemap tileMap = null;
    public List<Vector3> availablePlaces;

    private bool isAdjacent = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        /*
        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        actualPos = gridLayout.CellToWorld(cellPosition);
        */
        tileMap = transform.GetComponentInParent<Tilemap>();
        availablePlaces = new List<Vector3>();
        
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        
        /*
        foreach (var position in tileMap.cellBounds.allPositionsWithin)
        {
            if (!tileMap.HasTile(position))
            {
                continue;
            }
            availablePlaces.Add(position);
            // Tile is not empty; do stuff
        }
        */
        Debug.Log(availablePlaces.Count);
        /*
        foreach (Vector3 vec in availablePlaces)
        {
            Debug.Log(vec);
        }
        */
    }

    void FixedUpdate()
    {

        foreach(Vector3 vec in availablePlaces)
        {
            RaycastHit2D[] hits;
            hits = Physics2D.BoxCastAll(vec,new Vector2(1,1),0, -direction);
            /*
            if (hits.Length > 1)
            {
                if (hits[1].collider != null && hits[1].collider.gameObject.tag == "Player")
                {
                    player.transform.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
                    Debug.Log("Ray hit player");
                }
                else
                {
                    Debug.Log(hits[1].collider.gameObject.name);
                }
            }
            */
            //RaycastHit2D hit = Physics2D.Raycast(vec, -direction);
            
            foreach(RaycastHit2D hit in hits)
            {
                bool wall = false;
                if(hit.collider != null && hit.collider.gameObject.name.Contains("Walls"))
                {
                    if (wall)
                    {
                        break;
                    }
                    else
                    {
                        wall = true;
                        continue;
                    }
                }

                if (hit.collider != null && hit.collider.gameObject.tag == "Player" && !isAdjacent)
                {
                    player.transform.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
                    //Debug.Log("Ray hit player");
                }
                else
                {
                    //Debug.Log(hit.collider.gameObject.name);
                }
            }
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAdjacent = true;
        }
    }
}
