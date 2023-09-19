using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class ObjectsColiderTrigger : MonoBehaviour
{
    //[SerializeField]
    //private UnityEvent colliderTrigger;
    // Start is called before the first frame update
    public Tilemap tilemap;
    public GameObject firePrefab;
    public int numberofX;
    public int numberofY;
    private Vector2 position;
    public LayerMask Explosion;
    private void Start()
    {

        //    BoundsInt Bounds = new BoundsInt();

        //Bounds.xMin = 1;
        //Bounds.yMin = 1;
        //Bounds.xMax = 2;
        //Bounds.yMax = 2;

        //print(gameObject.GetComponent<BoxCollider2D>().bounds.min.x);
        //TileBase[] tileArray = tilemap.GetTilesBlock(Bounds);
        //for (int index = 0; index < tileArray.Length; index++)
        //{
        //    print(tileArray[index]);
        //}
        //print(tileArray.Length);
        position = transform.position;
  
    }

    private void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bomb")
        {
            position = GameObject.FindGameObjectWithTag("bomb").transform.position;
            Vector3 hitPosition = Vector3.zero;
            for (int i = 0; i < numberofX; i++)
            {
                for (int j = 0; j < numberofY; j++)
                {
                    foreach (ContactPoint2D hit in collision.contacts)
                    {
                        //Debug.Log(hit.point);
                        hitPosition.x = gameObject.GetComponent<BoxCollider2D>().bounds.min.x + (i+.01f);
                        hitPosition.y = gameObject.GetComponent<BoxCollider2D>().bounds.min.y + (j+.01f);
                        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                        
                    }
                }
               
            }
            Instantiate(firePrefab, position, Quaternion.identity);
            Destroy(this.gameObject);
        }

       /* if (Physics2D.OverlapBox(position, Vector2.one, 0f, Explosion))
        {
            Debug.Log("explosion");
            Vector3 hitPosition = Vector3.zero;
            for (int i = 0; i < numberofX; i++)
            {
                for (int j = 0; j < numberofY; j++)
                {
                    foreach (ContactPoint2D hit in collision.contacts)
                    {
                        //Debug.Log(hit.point);
                        hitPosition.x = gameObject.GetComponent<BoxCollider2D>().bounds.min.x + (i + .01f);
                        hitPosition.y = gameObject.GetComponent<BoxCollider2D>().bounds.min.y + (j + .01f);
                        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);

                    }
                }

            }

            //print(hitPosition);
            position = transform.position;
            Instantiate(firePrefab, position, Quaternion.identity);
            Destroy(this.gameObject);
        }*/
    }

    


}
