using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombMovement : MonoBehaviour
{
    public KeyCode inputKey = KeyCode.LeftShift;
    public float movespeed = 10;
    public float bombFuseTime = 5f;
    private GameObject Manager;
    private Vector2 currentDirection = new Vector2();
    private Vector2 position;
    private GameObject player1;
    private bool directionIsSet;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 3;

    [Header("Destructible")]
    private Tilemap destructibles;
    public Destructible destructiblePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillBomb());
        directionIsSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (directionIsSet == false)
        {
            direction();           
        }
    }

    private void direction ()
    {
        player1 = GameObject.FindGameObjectWithTag("Hamra");
        currentDirection = player1.gameObject.GetComponent<HamraController>().bombDirection;
        GetComponent<Rigidbody2D>().AddForce(currentDirection * movespeed);
        directionIsSet = true;
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "aragoz")
        {
            Physics2D.IgnoreCollision(GameObject.FindWithTag("aragoz").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (collision.gameObject.tag == "Destructibles")
        {
            position = transform.position;
            //position.x = Mathf.Round(position.x);
            //position.y = Mathf.Round(position.y);
            player1.GetComponent<HamraController>().Explosion();
            Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosion.SetActiveRenderer(explosion.start);
            explosion.DestroyAfter(explosionDuration);

            Explode(position, Vector2.up, explosionRadius);
            Explode(position, Vector2.down, explosionRadius);
            Explode(position, Vector2.left, explosionRadius);
            Explode(position, Vector2.right, explosionRadius);
            //player1.gameObject.GetComponent<HamraController>().bombsRemaining++;
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag == "wall")
        {
            //ReverseDirection();
            //Debug.Log("kill yourself");
            Physics2D.IgnoreCollision(GameObject.FindWithTag("wall").GetComponent<Collider2D>(), GetComponent<Collider2D>());

            //player1.gameObject.GetComponent<HamraController>().bombsRemaining++;
            //Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Item")
        {
            //ReverseDirection();
            //Debug.Log("kill yourself");
            Physics2D.IgnoreCollision(GameObject.FindWithTag("Item").GetComponent<Collider2D>(), GetComponent<Collider2D>());

            //player1.gameObject.GetComponent<HamraController>().bombsRemaining++;
            //Destroy(this.gameObject);
        }
    }
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            //ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    /*private void ClearDestructible(Vector2 position)
    {
        destructibles = player1.gameObject.GetComponent<HamraController>().destructibleTiles;
        Vector3Int cell = destructibles.WorldToCell(position);
        TileBase tile = destructibles.GetTile(cell);
        Debug.Log(position +""+ cell);

        if (tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibles.SetTile(cell, null);
        }
    }*/

    private IEnumerator KillBomb()
    {
        yield return new WaitForSeconds(bombFuseTime);
        player1.gameObject.GetComponent<HamraController>().bombsRemaining++;
        Destroy(this.gameObject);
    }


}
