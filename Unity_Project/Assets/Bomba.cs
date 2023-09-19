using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float speed = 1f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(transform.right * speed, ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Aragoz")
        {
            Destroy(GameObject.FindWithTag("Aragoz"));
            Destroy(this.gameObject);

        }
    }
}
