using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaloonMovement : MonoBehaviour
{
    public float movespeed = 10;
    private bool directionIsSet = false;
    private GameObject Manager;
    public Vector2 currentDirection = new Vector2();
    public Vector2 playerDirection = new Vector2();
    private GameObject player2;
    private Vector2 position;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public float bombFuseTime = 5f;
    public int explosionRadius = 3;

    [Header("Destructible")]
    private Tilemap destructibles;
    public Destructible destructiblePrefab;

    // Start is called before the first frame update
    void Start()
    {
        bombFuseTime = 1;
        StartCoroutine(KillBomb());
    }

    // Update is called once per frame
    void Update()
    {
        if (directionIsSet == false)
        {
            direction();
        }
    }

    private void direction()
    {
        player2 = GameObject.FindGameObjectWithTag("aragoz");
        currentDirection = player2.gameObject.GetComponent<AragozController>().baloonDirection;
        GetComponent<Rigidbody2D>().AddForce(currentDirection * movespeed);
        directionIsSet = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hamra")
        {
            Physics2D.IgnoreCollision(GameObject.FindWithTag("Hamra").GetComponent<Collider2D>(), GetComponent<Collider2D>());

        }
        else if (collision.gameObject.tag == "Destructibles")
        {
            position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            player2.GetComponent<AragozController>().Explosion();
            Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosion.SetActiveRenderer(explosion.start);
            explosion.DestroyAfter(explosionDuration);

            Explode(position, Vector2.up, explosionRadius);
            Explode(position, Vector2.down, explosionRadius);
            Explode(position, Vector2.left, explosionRadius);
            Explode(position, Vector2.right, explosionRadius);
            //player1.gameObject.GetComponent<AragozController>().bombsRemaining++;

            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag == "wall")
        {
            Physics2D.IgnoreCollision(GameObject.FindWithTag("wall").GetComponent<Collider2D>(), GetComponent<Collider2D>());
            //Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag == "Building")
        {
            Physics2D.IgnoreCollision(GameObject.FindWithTag("Building").GetComponent<Collider2D>(), GetComponent<Collider2D>());
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
        destructibles = player2.gameObject.GetComponent<AragozController>().destructibleTiles;
        Vector3Int cell = destructibles.WorldToCell(position);
        TileBase tile = destructibles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibles.SetTile(cell, null);
        }
    }*/

    private IEnumerator KillBomb()
    {
        yield return new WaitForSeconds(bombFuseTime);
        position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);
        //player1.gameObject.GetComponent<AragozController>().bombsRemaining++;
        Destroy(this.gameObject);
    }
}
