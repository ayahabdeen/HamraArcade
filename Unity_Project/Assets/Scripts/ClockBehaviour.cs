using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClockBehaviour : MonoBehaviour
{
    public float movespeed = 10;
    private bool directionIsSet = false;
    private GameObject Manager;
    public Vector2 currentDirection = new Vector2();
    public Vector2 playerDirection = new Vector2();
    private GameObject player2;
    private Vector2 position;
    private float bombFuseTime;

    // Start is called before the first frame update
    void Start()
    {
        bombFuseTime = 5;
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
        player2 = GameObject.FindGameObjectWithTag("Hamra");
        currentDirection = player2.gameObject.GetComponent<HamraController>().bombDirection;
        GetComponent<Rigidbody2D>().AddForce(currentDirection * movespeed);
        directionIsSet = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "aragoz")
        {

            Manager = GameObject.FindGameObjectWithTag("gameManager");
            Manager.gameObject.GetComponent<GameManager>().isAragoz = true;
            Manager.gameObject.GetComponent<GameManager>().SetSliderValue();
            //player1.gameObject.GetComponent<AragozController>().bombsRemaining++;
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Destructibles")
        {
            Physics2D.IgnoreCollision(GameObject.FindWithTag("Destructibles").GetComponent<Collider2D>(), GetComponent<Collider2D>());
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
        else if (collision.gameObject.tag == "Item")
        {
            Physics2D.IgnoreCollision(GameObject.FindWithTag("Item").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }


    private IEnumerator KillBomb()
    {
        yield return new WaitForSeconds(bombFuseTime);
        //player2.gameObject.GetComponent<AragozController>().bombsRemaining++;
        Destroy(this.gameObject);
    }
}
