using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class AragozController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public Vector2 direction = Vector2.down;
    public Vector2 baloonDirection;
    public float speed = 50;
    public Tilemap destructibleTiles;
    public int lives = 0;
    public GameObject pauseBombs;
    public bool bombsPaused;

    [Header("Bomb")]
    public KeyCode inputKey = KeyCode.LeftShift;
    public GameObject bombPrefab;
    private GameObject bomb;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    public int bombsRemaining = 5;
    private Vector2 position;

    [Header("kiss")]
    public KeyCode kissKey = KeyCode.LeftAlt;
    public GameObject kissPrefab;
    private GameObject kiss;
    public float kissFuseTime = 3f;
    //public int kissAmount = 1;
    public int kissRemaining = 5;
    public Text kissAmount;
    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Input")]
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    [Header("Sprites")]
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    [Header("Sounds")]
    public AudioClip aragozAttackClip;
    public AudioClip bombClip;
    public AudioClip aragozIsHit;
    private AudioSource radio;




    public void Start()
    {
        baloonDirection = Vector2.right;
        bombAmount = 5;
        pauseBombs.SetActive(false);
        bombsPaused = false;
        radio = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    public void Update()
    {
        if (FindObjectOfType<GameManager>().gameIsPaused == false)
        {
            if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
            {
                generateBomb();
            }

            if (bombsPaused == true)
            {
                StartCoroutine(ResertBombs());
            }

            if (kissRemaining > 0 && Input.GetKeyDown(kissKey))
            {
                generateKiss();
            }

            if (Input.GetKey(inputUp))
            {
                SetDirection(Vector2.up, spriteRendererUp);
                baloonDirection = Vector2.up;
            }
            else if (Input.GetKey(inputDown))
            {
                SetDirection(Vector2.down, spriteRendererDown);
                baloonDirection = Vector2.down;
            }
            else if (Input.GetKey(inputLeft))
            {
                SetDirection(Vector2.left, spriteRendererLeft);
                baloonDirection = Vector2.left;
            }
            else if (Input.GetKey(inputRight))
            {
                SetDirection(Vector2.right, spriteRendererRight);
                baloonDirection = Vector2.right;
            }
            else if (Input.GetKey(inputDown) && Input.GetKey(inputLeft))
            {
                SetDirection(Vector2.down, spriteRendererDown);
                baloonDirection = Vector2.zero;
            }
            else if (Input.GetKey(inputUp) && Input.GetKey(inputLeft))
            {
                SetDirection(Vector2.up, spriteRendererUp);
                baloonDirection = Vector2.zero;
            }
            else if (Input.GetKey(inputDown) && Input.GetKey(inputRight))
            {
                SetDirection(Vector2.down, spriteRendererDown);
                baloonDirection = Vector2.zero;
            }
            else if (Input.GetKey(inputUp) && Input.GetKey(inputRight))
            {
                SetDirection(Vector2.up, spriteRendererUp);
                baloonDirection = Vector2.zero;
            }
            else
            {
                SetDirection(Vector2.zero, activeSpriteRenderer);
            }
        }
    }

    private void generateBomb()
    {
        position = transform.position;
        bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        Physics2D.IgnoreCollision(bomb.GetComponent<Collider2D>(), GetComponent<Collider2D>());
       // radio.clip = bombClip;
      //  radio.Play();

        bombsRemaining--;
        if (bombsRemaining == 0)
        {
            bombsPaused = true;
            Debug.Log("Aragoz" + bombsRemaining);
        }
        
    }
    private void generateKiss()
    {
        position = transform.position;
        kiss = Instantiate(kissPrefab, position, Quaternion.identity);
        Physics2D.IgnoreCollision(kiss.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        radio.clip = aragozAttackClip;
        GetComponent<AudioSource>().Play();
        kissRemaining--;
        kissAmount.text = kissRemaining.ToString();
    }

    public void AddBomb()
    {
        bombsRemaining = 5;
    }

    public void AddKiss()
    {
        kissRemaining = 5;
    }
    public void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;
        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
    public void SpeedIncrease()
    {
        StartCoroutine(IncreaseSpeed());
    }
    public void SpeedDecrease()
    {
        StartCoroutine(DecreaseSpeed());
    }
    public IEnumerator IncreaseSpeed()
    {
        speed = speed + 50;
        yield return new WaitForSeconds(20);
        speed = 50;
    }
    public IEnumerator DecreaseSpeed()
    {
        speed = speed - 2;
        yield return new WaitForSeconds(10);
        speed = 5;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
    public IEnumerator ResertBombs()
    {   
        pauseBombs.SetActive(true);
        yield return new WaitForSeconds(20);
        bombsRemaining = 5;
        bombsPaused = true;
        pauseBombs.SetActive(false);
    }
    public void AragozCry()
    {
        radio.clip = aragozIsHit;
        radio.Play();
    }
    public void Explosion()
    {
        radio.clip = bombClip;
        radio.Play();
    }
}
