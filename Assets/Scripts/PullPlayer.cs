using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PullPlayer : MonoBehaviour
{
    Rigidbody2D playerRB;
    public float force = 1.0f;
    public Vector2 direction = new Vector2(1, 0);

    public Tilemap tileMap = null;
    public List<Vector3> availablePlaces;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
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
            }
        }
    }

    void FixedUpdate()
    {

        foreach(Vector3 vec in availablePlaces)
        {
            RaycastHit2D[] hits;
            hits = Physics2D.BoxCastAll(vec,new Vector2(1,1),0, -direction);

            bool wall = false;
            foreach (RaycastHit2D hit in hits)
            {
                bool hitWall = hit.collider != null && hit.collider.gameObject.name.Contains("Walls");
                if (hitWall)
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
                bool hitPlayer = hit.collider != null && hit.collider.gameObject.tag == "Player";
                if (hitPlayer)
                {
                    playerRB.AddForce(direction * force, ForceMode2D.Impulse);
                }
            }
            
        }
        
    }
}
